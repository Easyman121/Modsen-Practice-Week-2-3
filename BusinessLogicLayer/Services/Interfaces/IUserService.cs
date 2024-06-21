using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface IUserService
{
    Task SetUserAsync(UserRequestDto userDto, CancellationToken cancellationToken);
    Task<UserResponseDto> GetUserAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<UserResponseDto>> GetUsersAsync(CancellationToken cancellationToken);
    Task<UserResponseDto> LoginAsync(UserRequestDto userDto, CancellationToken cancellationToken);
    Task<UserResponseDto> RegistrationAsync(UserRequestDto userDto, CancellationToken cancellationToken);
}