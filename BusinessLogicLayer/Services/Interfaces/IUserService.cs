using DataAccessLayer.Models;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<ValidationResult> CreateUserAsync(Users user);
        // other methods?
    }
}
