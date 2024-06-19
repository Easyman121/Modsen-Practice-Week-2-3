using FluentValidation;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Validators
{
    public class UserValidator : AbstractValidator<Users>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .Length(2, 50).WithMessage("Your {PropertyName} length of {TotalLength} is beyond the acceptable range from 2 to 50")
                .Must(BeAValidUsername).WithMessage("{PropertyName} contains invalid characters!");

            RuleFor(u => u.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .EmailAddress().WithMessage("Invalid {PropertyName} format!");

            // TO DO: make a proper hash validation
            RuleFor(u => u.PasswordHash)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");
        }

        protected bool BeAValidUsername(string username)
        {
            username = username.Replace("_", "").Replace("-", "");
            //checks in unicode
            return username.All(char.IsLetterOrDigit);
        }
    }
}
/*

BindingList <strin> errors = new BindingList <string>();

UserValidator validator = new UserValidator();

ValidationResult results = validator.Validate(user);

if(results.IsValid == false)
{
    foreach (ValidationFailure failure in results.Errors)
    {
        errors.Add(failure.ErrorMessage);
    }
}
 */
