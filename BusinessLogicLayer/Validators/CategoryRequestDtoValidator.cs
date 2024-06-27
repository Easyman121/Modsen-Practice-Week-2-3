using BusinessLogicLayer.DTO.Request;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class CategoryRequestDtoValidator : AbstractValidator<CategoryRequestDto>
{
    public CategoryRequestDtoValidator()
    {
        RuleFor(c => c.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is empty!")
            .Length(2, 32)
            .WithMessage("The length of {PropertyName} must be between 2 and 32 characters. The current length is {TotalLength}")
            .Matches("^[A-Z][a-z\\-_0-9]*$").WithMessage("{PropertyName} should contain only letters and numbers.")
            .Must(StartWithCapitalLetter).WithMessage("{PropertyName} should start with a capital letter.");
    }

    private bool StartWithCapitalLetter(string name) => char.IsUpper(name.FirstOrDefault());
}