using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IAuthService
{
    Task<UserResponseDto> LoginAsync(UserRequestDto userDto, CancellationToken cancellationToken);
    Task<UserResponseDto> RegistrationAsync(UserRequestDto userDto, CancellationToken cancellationToken);
}