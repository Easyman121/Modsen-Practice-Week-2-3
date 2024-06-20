using FluentValidation;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Validators
{
    public class CategoryValidator : AbstractValidator<Categories>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .Length(2, 50).WithMessage("The length of {PropertyName} must be between 2 and 50 characters. The current length is {TotalLength}")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("{PropertyName} should contain only letters and numbers.")
                .Must(StartWithCapitalLetter).WithMessage("{PropertyName} should start with a capital letter.");
        }

        private bool StartWithCapitalLetter(string name)
        {
            return char.IsUpper(name.FirstOrDefault());
        }
    }
}
