using FluentValidation;
using DataAccessLayer.Models;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer.Validators
{
    public class ProductValidator : AbstractValidator<Products>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .Length(2, 100).WithMessage("The length of {PropertyName} must be between 2 and 100 characters. The current length is {TotalLength}")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("{PropertyName} contains invalid characters!");

            RuleFor(p => p.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .Length(10, 500).WithMessage("The length of {PropertyName} must be between 10 and 500 characters. The current length is {TotalLength}")
                .Must(description => !ContainsForbiddenWords(description)).WithMessage("{PropertyName} contains forbidden words!");

            RuleFor(p => p.Price)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0")
                .LessThanOrEqualTo(1000000).WithMessage("{PropertyName} should be less than or equal to 1,000,000");

            RuleFor(p => p.Category)
                .NotNull().WithMessage("Category is required.")
                .SetValidator(new CategoryValidator());
        }

        private bool ContainsForbiddenWords(string description)
        {
            // Define a list of forbidden words
            var forbiddenWords = new[] { "forbidden", "invalid", "restricted" };
            foreach (var word in forbiddenWords)
            {
                if (description.Contains(word, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
