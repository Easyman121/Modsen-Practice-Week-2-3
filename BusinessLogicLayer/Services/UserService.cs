﻿using AutoMapper;
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
        var allUsers = await ServiceHelper.CheckAndGetEntitiesAsync(DataBase.User.GetAllAsync, cancellationToken);
        NonUniqueException.EnsureUnique(allUsers, c => c.UserName == userDto.UserName, "Username is already taken");
        NonUniqueException.EnsureUnique(allUsers, c => c.Email == userDto.Email, "Email is already taken");
        var user = _mapper.Map<User>(userDto);

        await DataBase.User.InsertAsync(user, cancellationToken);
    }

    public async Task UpdateUserAsync(int id, UserRequestDto userDto, CancellationToken cancellationToken)
    {
        CheckFields(userDto, cancellationToken);
        var userOld =
            await ServiceHelper.CheckAndGetEntityAsync(DataBase.User.GetByIdAsync, id, cancellationToken);

        var allUsers = await ServiceHelper.CheckAndGetEntitiesAsync(DataBase.User.GetAllAsync, cancellationToken);

        if (userOld.Email != userDto.Email)
        {
            NonUniqueException.EnsureUnique(allUsers, c => c.Email == userDto.Email, "Email is already taken");
        }

        if (userOld.UserName != userDto.UserName)
        {
            NonUniqueException.EnsureUnique(allUsers, c => c.UserName == userDto.UserName, "Username is already taken");
        }

        var userNew = _mapper.Map<User>(userDto);
        userNew.Id = id;
        await DataBase.User.UpdateAsync(userNew, cancellationToken);
    }

    public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await ServiceHelper.CheckAndGetEntityAsync(DataBase.User.GetByIdAsync, id, cancellationToken);
        await DataBase.User.DeleteAsync(user, cancellationToken);
    }

    public async Task<UserResponseDto> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await ServiceHelper.CheckAndGetEntityAsync(DataBase.User.GetByIdAsync, id, cancellationToken);
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<IEnumerable<UserResponseDto>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var users = await ServiceHelper.CheckAndGetEntitiesAsync(DataBase.User.GetAllAsync, cancellationToken);

        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    private static void CheckFields(UserRequestDto userDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfNullOrWhiteSpace(userDto.Email);
        RequestDtoException.ThrowIfNullOrWhiteSpace(userDto.UserName);
        RequestDtoException.ThrowIfNull(userDto.PasswordHash);
    }
}