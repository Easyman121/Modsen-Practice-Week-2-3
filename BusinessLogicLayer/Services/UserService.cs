using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

internal class UserService : IUserService
{
    private IUnitOfWork DataBase { get; set; }
    private IMapper _mapper = new MapperConfiguration(x => x.AddProfile<AppMappingProfile>()).CreateMapper();

    public async Task InsertUserAsync(UserRequestDto userDto, CancellationToken cancellationToken)
    {
        CheckFields(userDto, cancellationToken);
        var allUsers = await DataBase.User.GetAllAsync(cancellationToken);
        if (allUsers.Exists(c => c.UserName == userDto.UserName))
        {
            throw new NonUniqueException("Username is already taken");
        }

        if (allUsers.Exists(c => c.Email == userDto.Email))
        {
            throw new NonUniqueException("Email is already registered");
        }

        var user = _mapper.Map<User>(userDto);

        await DataBase.User.InsertAsync(user, cancellationToken);
    }

    public async Task UpdateUserAsync(int id, UserRequestDto userDto, CancellationToken cancellationToken)
    {
        CheckFields(userDto, cancellationToken);
        var user = await CheckAndGetUser(id, cancellationToken);
        var allUsers = await DataBase.User.GetAllAsync(cancellationToken);
        if (user.Email != userDto.Email)
        {
            if (allUsers.Exists(c => c.Email == userDto.Email))
            {
                throw new NonUniqueException("Email is already registered");
            }
        }

        if (user.UserName != userDto.UserName)
        {
            if (allUsers.Exists(c => c.UserName == userDto.UserName))
            {
                throw new NonUniqueException("Username is already taken");
            }
        }

        var userf = _mapper.Map<User>(userDto);
        userf.Id = id;
        DataBase.User.UpdateAsync(userf, cancellationToken);
    }

    public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await CheckAndGetUser(id, cancellationToken);
        DataBase.User.DeleteAsync(user, cancellationToken);
    }

    public async Task<UserResponseDto> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await CheckAndGetUser(id, cancellationToken);
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<IEnumerable<UserResponseDto>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var users = await DataBase.User.GetAllAsync(cancellationToken);

        foreach (var user in users)
        {
            if (user == null)
            {
                throw new RequestDtoException("The entry is empty");
            }
        }

        if (users == null)
        {
            throw new RequestDtoException("The list is empty");
        }


        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    private static void CheckFields(UserRequestDto userDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        if (userDto.Email == null)
        {
            throw new RequestDtoException("No email provided");
        }

        if (userDto.UserName == null)
        {
            throw new RequestDtoException("No username provided");
        }
    }

    private async Task<User> CheckAndGetUser(int id, CancellationToken cancellationToken)
    {
        if (id < 0)
        {
            throw new ArgumentException("Id is lower than 0");
        }

        var user = await DataBase.User.GetByIdAsync(id, cancellationToken);
        if (user == null)
        {
            throw new RequestDtoException("No user entries found");
        }

        return user;
    }
}