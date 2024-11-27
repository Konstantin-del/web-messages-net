
using Messages.Bll.ModelsBll;

namespace Messages.Bll.Interfaces;

public interface IUserService
{
    public Task<UserDto> AuthenticateUserAsync(AuthenticateDto dataAuth);

    public Task<UserDto> CreateUserAsync(RegisterDto newUser);
}
