using BusinessLogicLayer.DTO.Request;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class OrderRequestDtoValidator : AbstractValidator<OrderRequestDto>
{
    public OrderRequestDtoValidator()
    {
        RuleFor(o => o.DateTime)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is empty!")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("{PropertyName} cannot be in the future")
            .GreaterThanOrEqualTo(DateTime.Now.AddYears(-20))
            .WithMessage("{PropertyName} cannot be earlier than 20 years ago");

        RuleFor(o => o.UserName)
            .NotEmpty().WithMessage("UserName is required.");
    }
}