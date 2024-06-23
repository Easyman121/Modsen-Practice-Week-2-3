using BusinessLogicLayer.DTO.Request;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class OrderItemRequestDtoValidator : AbstractValidator<OrderItemRequestDto>
{
    public OrderItemRequestDtoValidator()
    {
        RuleFor(oi => oi.Product)
            .NotNull().WithMessage("Product is required.")
            .SetValidator(new ProductRequestDtoValidator());

        RuleFor(oi => oi.Count)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0")
            .LessThanOrEqualTo(1000).WithMessage("{PropertyName} should be less than or equal to 1000");
    }
}