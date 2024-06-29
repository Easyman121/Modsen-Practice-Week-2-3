using BusinessLogicLayer.Authentification;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IUserService
{
    Task<AuthenticateResponse> AuthenticateAsync(UserRequestDto userRequest, CancellationToken cancellationToken);
    Task<int> InsertUserAsync(UserRequestDto userDto, CancellationToken cancellationToken);
    Task UpdateUserAsync(int id, UserRequestDto userDto, CancellationToken cancellationToken);
    Task DeleteUserAsync(int id, CancellationToken cancellationToken);
    Task<UserResponseDto> GetUserAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<UserResponseDto>> GetUsersAsync(CancellationToken cancellationToken);
    Task<IEnumerable<OrderResponseDto>> GetOrdersAsync(int userId, CancellationToken cancellationToken);
}