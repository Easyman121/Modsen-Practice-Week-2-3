using FluentValidation;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItems>
    {
        public OrderItemValidator()
        {
            RuleFor(oi => oi.Product)
                .NotNull().WithMessage("Product is required.")
                .SetValidator(new ProductValidator());

            RuleFor(oi => oi.Order)
                .NotNull().WithMessage("Order is required.");

            RuleFor(oi => oi.Count)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0")
                .LessThanOrEqualTo(1000).WithMessage("{PropertyName} should be less than or equal to 1000");

        }
    }
}
