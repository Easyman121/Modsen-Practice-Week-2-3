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

            RuleFor(oi => oi.Count)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");
        }
    }
}
