
using Messages.Bll.ModelsBll;
using Messages.Dal.Entityes;
using System.Runtime.CompilerServices;

namespace Messages.Bll.Interfaces;

public interface IUserService
{
    Task<UserDto> AuthenticateUserAsync(AuthenticateDto dataAuth);

    Task<UserDto> CreateUserAsync(RegisterDto newUser);

    Task<UpdateUserDto> UpdateUserAsync(Guid id, UpdateUserDto userName);

    Task DeleteUserAsync(Guid id);
}
