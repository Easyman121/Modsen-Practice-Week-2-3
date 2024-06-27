using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface IUserService
{
    Task InsertUserAsync(UserRequestDto userDto, CancellationToken cancellationToken);
    Task UpdateUserAsync(int id, UserRequestDto userDto, CancellationToken cancellationToken);
    Task DeleteUserAsync(int id, CancellationToken cancellationToken);
    Task<UserResponseDto> GetUserAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<UserResponseDto>> GetUsersAsync(CancellationToken cancellationToken);
    Task<IEnumerable<OrderResponseDto>> GetOrdersAsync(int userId, CancellationToken cancellationToken);
    Task<OrderResponseDto> GetOrderAsync(int orderId, CancellationToken cancellationToken);
}