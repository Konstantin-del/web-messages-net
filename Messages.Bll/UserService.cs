using AutoMapper;
using Messages.Bll.ModelsBll;
using Messages.Dal.Entityes;
using Messages.Bll.Interfaces;
using Messages.Dal.Interfaces;
using Messages.Bll.Exceptions;

namespace Messages.Bll;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    IPasswordHelper passwordHelper
) : IUserService
{ 
    public async Task<UserDto> AuthenticateUserAsync(AuthenticateDto dataAuth)
    {
        var user = await userRepository.AuthenticateUserAsync(dataAuth.Nick);
        var f = user;
       
        if (user != null && passwordHelper.VerifyPassword(dataAuth.Password, user.Password, user.Salt))
            return mapper.Map<UserDto>(user);
        else
            throw new EntityNotFoundException("password or login does not found");
    }

    public async Task<UserDto> CreateUserAsync(RegisterDto user)
    {
        var entity = await userRepository.GetUserByNickAsync(user.Nick);
        if (entity != null)
            throw new UserAlreadyExistsException("this nick already exists");
        var result = mapper.Map<UserEntity>(user);
        result.Password = passwordHelper.HashPasword(result.Password, out var salt);
        result.Salt = salt;
        result.RegistrationDate = DateTimeOffset.UtcNow;
        var newUser = await userRepository.CreateUserAsync(result);
        if (newUser is null)
            throw new FailedToCreateException("failed to create user");
        var readyUser = mapper.Map<UserDto>(newUser);
        return readyUser;
    }

    public async Task<UpdateUserDto> UpdateUserAsync(Guid id, UpdateUserDto item)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        if (user is null)
            throw new EntityNotFoundException("user not found");
        var newItem = mapper.Map<UpdateUserEntity>(item);
        var result = await userRepository.UpdateUserAsync(id, newItem);
        if (result is null)
            throw new FailedToCreateException("failed to update user");
        UpdateUserDto updateItem = new();
        updateItem.Name = result.Name;
        return updateItem;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        if (user is null)
            throw new EntityNotFoundException("user not found");
        await userRepository.DeleteUserAsync(id);
    }
}
