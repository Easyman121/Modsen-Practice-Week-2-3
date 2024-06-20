using FluentValidation;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Validators
{
    public class UserValidator : AbstractValidator<Users>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .Length(2, 50).WithMessage("Your {PropertyName} length of {TotalLength} is beyond the acceptable range from 2 to 50")
                .Must(BeAValidUsername).WithMessage("{PropertyName} contains invalid characters!");

            RuleFor(u => u.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .EmailAddress().WithMessage("Invalid {PropertyName} format!");

            // // Validation for password input
            //RuleFor(u => u.Password)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty().WithMessage("{PropertyName} is empty!")
            //    .MinimumLength(6).WithMessage("{PropertyName} should be at least 6 characters long.")
            //    .Matches(@"[A-Z]").WithMessage("{PropertyName} must contain at least one uppercase letter.")
            //    .Matches(@"[a-z]").WithMessage("{PropertyName} must contain at least one lowercase letter.")
            //    .Matches(@"[0-9]").WithMessage("{PropertyName} must contain at least one number.")
            //    .Matches(@"[\!\?\*\.]").WithMessage("{PropertyName} must contain at least one special character (!? *.).");

            //// Validation for password hash (string)
            //RuleFor(u => u.PasswordHash)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty().WithMessage("{PropertyName} is empty!")
            //    .Length(64).WithMessage("{PropertyName} should be exactly 64 characters long.")
            //    .Matches("^[a-fA-F0-9]+$").WithMessage("{PropertyName} should contain only hexadecimal characters.");

            // Validation for password hash
            RuleFor(u => u.PasswordHash)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is empty!")
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0.")
                .Must(BeAValidHash).WithMessage("{PropertyName} is not a valid hash.");
        }

        protected bool BeAValidUsername(string username)
        {
            username = username.Replace("_", "").Replace("-", "");
            return username.All(char.IsLetterOrDigit);
        }

        protected bool BeAValidHash(int passwordHash)
        {
            // Assuming the int value must be within a certain range
            return passwordHash > 10 && passwordHash < 999999; 
        }
    }
}