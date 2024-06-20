using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using BusinessLogicLayer.Services.Interfaces;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<Users> _userValidator;

        public UserService(IUserRepository userRepository, IValidator<Users> userValidator)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
        }

        public async Task<ValidationResult> CreateUserAsync(Users user)
        {
            ValidationResult validationResult = await _userValidator.ValidateAsync(user);

            if (validationResult.IsValid)
            {
                // if validation successfull then save user to bd
                await _userRepository.Insert(user);
            }

            return validationResult;
        }

        // other methods?
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