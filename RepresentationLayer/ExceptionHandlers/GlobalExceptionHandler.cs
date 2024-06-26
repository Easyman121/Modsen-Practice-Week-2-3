using System.Text.Json;
using System.Text.Json.Serialization;
using BusinessLogicLayer.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace RepresentationLayer.ExceptionHandlers;

public class GlobalExceptionHandler(IHostEnvironment environment, ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, exception.Message);

        httpContext.Response.StatusCode = exception switch
        {
            TestCustomException => 451,
            _ => httpContext.Response.StatusCode
        };

        var problem = CreateProblemDetails(httpContext, exception);
        var json = ToJson(problem);
        const string contentType = "application/problem+json";
        httpContext.Response.ContentType = contentType;

        await httpContext.Response.WriteAsync(json, cancellationToken);

        return true;
    }

    private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        var statusCode = context.Response.StatusCode;
        var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
        if (string.IsNullOrEmpty(reasonPhrase))
        {
            reasonPhrase = UnhandledExceptionMsg;
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = reasonPhrase
        };

        if (!environment.IsDevelopment())
        {
            return problemDetails;
        }

        problemDetails.Detail = exception.Message;
        problemDetails.Extensions["traceId"] = context.TraceIdentifier;
        problemDetails.Extensions["data"] = exception.Data;

        return problemDetails;
    }

    private string ToJson(ProblemDetails problemDetails)
    {
        try
        {
            return JsonSerializer.Serialize(problemDetails, SerializerOptions);
        }
        catch (Exception exception)
        {
            const string msg = "An exception has occurred while serializing error to JSON";
            logger.LogError(exception, msg);
        }

        return string.Empty;
    }
}