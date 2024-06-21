using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface IUserService
{
    Task SetUserAsync(UserRequestDto userDto);
    Task<UserResponseDto> GetUserAsync(int id);
    Task<IEnumerable<UserResponseDto>> GetUsersAsync();
}