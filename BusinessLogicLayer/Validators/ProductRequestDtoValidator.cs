using BusinessLogicLayer.DTO.Request;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class ProductRequestDtoValidator : AbstractValidator<ProductRequestDto>
{
    public ProductRequestDtoValidator()
    {
        RuleFor(p => p.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is empty!")
            .Length(2, 32)
            .WithMessage(
                "The length of {PropertyName} must be between 2 and 32 characters. The current length is {TotalLength}")
            .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("{PropertyName} contains invalid characters!");

        RuleFor(p => p.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is empty!")
            .Length(10, 256)
            .WithMessage(
                "The length of {PropertyName} must be between 10 and 256 characters. The current length is {TotalLength}")
            .Must(description => !ContainsForbiddenWords(description))
            .WithMessage("{PropertyName} contains forbidden words!");

        RuleFor(p => p.Price)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0")
            .LessThanOrEqualTo(1000000).WithMessage("{PropertyName} should be less than or equal to 1,000,000");

        RuleFor(p => p.CategoryName)
            .NotEmpty().WithMessage("Category name is required.");
    }

    public static bool ContainsForbiddenWords(string description)
    {
        var forbiddenWords = new[] { "forbidden", "invalid", "restricted" };
        return forbiddenWords.Any(word => description.Contains(word, StringComparison.OrdinalIgnoreCase));
    }
}