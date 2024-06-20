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

            //// To eliminate duplicates?
            //RuleFor(oi => oi)
            //    .Must((oi, context) =>
            //        !oi.Order.OrderItems.Any(item => item.Product == oi.Product && item != oi))
            //    .WithMessage("Duplicate products are not allowed in the same order.");

        }
    }
}
