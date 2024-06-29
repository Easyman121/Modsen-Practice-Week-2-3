using AutoMapper;
using BusinessLogicLayer.Authentification;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BusinessLogicLayer.Services;

public class UserService(IUnitOfWork uow, IMapper mapper) : IUserService
{
    public async Task<AuthenticateResponse> AuthenticateAsync(UserRequestDto userRequest, CancellationToken cancellationToken)
    {
        var user = (await ServiceHelper.GetEntitiesAsync(uow.User.GetAllAsync, cancellationToken)).SingleOrDefault(
            x => x.Email == userRequest.Email
            && Enumerable.SequenceEqual(x.PasswordHash, userRequest.PasswordHash));

        var token = await GenerateJwtTokenAsync(user);

        return new AuthenticateResponse
        {
            User = mapper.Map<UserResponseDto>(user),
            Token = token
        };

    }
    public async Task<int> InsertUserAsync(UserRequestDto userDto, CancellationToken cancellationToken)
    {
        CheckFieldsAndToken(userDto, cancellationToken);
        var allUsers = await ServiceHelper.GetEntitiesAsync(uow.User.GetAllAsync, cancellationToken);
        if (allUsers.Count > 0)
        {
            NonUniqueException.EnsureUnique(allUsers, c => c.UserName == userDto.UserName, "Username is already taken");
            NonUniqueException.EnsureUnique(allUsers, c => c.Email == userDto.Email, "Email is already taken");
        }

        var user = mapper.Map<User>(userDto);

        return await uow.User.InsertAsync(user, cancellationToken);
    }

    public async Task UpdateUserAsync(int id, UserRequestDto userDto, CancellationToken cancellationToken)
    {
        CheckFieldsAndToken(userDto, cancellationToken);
        var user =
            await ServiceHelper.CheckAndGetEntityAsync(uow.User.GetByIdAsync, id, cancellationToken);

        var allUsers = await ServiceHelper.GetEntitiesAsync(uow.User.GetAllAsync, cancellationToken);
        if (user.Email != userDto.Email)
        {
            NonUniqueException.EnsureUnique(allUsers, c => c.Email == userDto.Email, "Email is already taken");
        }

        if (user.UserName != userDto.UserName)
        {
            NonUniqueException.EnsureUnique(allUsers, c => c.UserName == userDto.UserName,
                "Username is already taken");
        }

        user.Email = userDto.Email;
        user.UserName = userDto.UserName;
        user.PasswordHash = user.PasswordHash;

        await uow.User.UpdateAsync(user, cancellationToken);
    }

    public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await ServiceHelper.CheckAndGetEntityAsync(uow.User.GetByIdAsync, id, cancellationToken);
        await uow.User.DeleteAsync(user, cancellationToken);
    }

    public async Task<UserResponseDto> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await ServiceHelper.CheckAndGetEntityAsync(uow.User.GetByIdAsync, id, cancellationToken);
        return mapper.Map<UserResponseDto>(user);
    }

    public async Task<IEnumerable<UserResponseDto>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var users = await ServiceHelper.GetEntitiesAsync(uow.User.GetAllAsync, cancellationToken);

        return mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetOrdersAsync(int userId, CancellationToken cancellationToken)
    {
        var orders = await uow.Order.GetOrdersByUserAsync(userId, cancellationToken);
        return mapper.Map<IEnumerable<OrderResponseDto>>(orders);
    }

    private static void CheckFieldsAndToken(UserRequestDto userDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfNullOrWhiteSpace(userDto.Email);
        RequestDtoException.ThrowIfNullOrWhiteSpace(userDto.UserName);
        RequestDtoException.ThrowIfNull(userDto.PasswordHash);
    }

    private async Task<string> GenerateJwtTokenAsync(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(AuthConfiguration.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });

        return tokenHandler.WriteToken(token);
    }
}