using System.Runtime.CompilerServices;

namespace BusinessLogicLayer.Exceptions;

public class RequestDtoException : Exception
{
    public RequestDtoException(string message) : base(message)
    {
    }

    public static void ThrowIfNullOrWhiteSpace(string? value,
        [CallerArgumentExpression(nameof(value))]
        string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new RequestDtoException($"The parameter '{paramName}' cannot be null, empty or whitespaces");
        }
    }

    public static void ThrowIfLessThan<T>(T value, T other,
        [CallerArgumentExpression(nameof(value))]
        string? paramName = null) where T : IComparable<T>
    {
        if (value.CompareTo(other) < 0)
        {
            throw new RequestDtoException($"The value {paramName}={value} is less than {other}");
        }
    }
}