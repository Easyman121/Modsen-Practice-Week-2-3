using BusinessLogicLayer.DTO.Request;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
{
    public UserRequestDtoValidator()
    {
        RuleFor(u => u.UserName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is empty!")
            .Length(2, 50)
            .WithMessage("Your {PropertyName} length of {TotalLength} is beyond the acceptable range from 2 to 50")
            .Must(BeAValidUsername).WithMessage("{PropertyName} contains invalid characters!");

        RuleFor(u => u.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is empty!")
            .EmailAddress().WithMessage("Invalid {PropertyName} format!");

        RuleFor(u => u.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is empty!");
    }

    protected bool BeAValidUsername(string username)
    {
        username = username.Replace("_", "").Replace("-", "");
        return username.All(char.IsLetterOrDigit);
    }
}