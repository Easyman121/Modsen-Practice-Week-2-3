using FluentValidation;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Validators
{
    public class ProductValidator : AbstractValidator<Products>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .Length(2, 100).WithMessage("The length of {PropertyName} must be between 2 and 100 characters. The current length is {TotalLength}");

            RuleFor(p => p.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .Length(10, 500).WithMessage("The length of {PropertyName} must be between 10 and 500 characters. The current length is {TotalLength}");

            RuleFor(p => p.Price)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");

            RuleFor(p => p.Category)
                .NotNull().WithMessage("Category is required.")
                .SetValidator(new CategoryValidator());
        }
    }
}
