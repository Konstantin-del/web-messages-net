using AutoMapper;
using Messages.Bll.Mappings;
using Messages.Bll.ModelsBll;
using Messages.Dal.Entityes;
using Messages.Bll.Interfaces;
using Messages.Dal.Interfaces;

namespace Messages.Bll;

public class UserService : IUserService
{
    IUserRepository _userRepository;
    PasswordHelper _passvordHelper;
    Mapper _mapper;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passvordHelper = new();

        var config = new MapperConfiguration(
        cfg =>
        {
            cfg.AddProfile(new UserMapperProfileBll());
        });

        _mapper = new Mapper(config);
    }

    public async Task<UserDto> AuthenticateUserAsync(AuthenticateDto dataAuth)
    {
        _passvordHelper = new();
       
        var user = await _userRepository.AuthenticateUserAsync(dataAuth.Nick);

        if (user is null)
        { 
            return null;
        }
        if(_passvordHelper.VerifyPassword(dataAuth.Password, user.Password, user.Salt))
        {
            var result = _mapper.Map<UserDto>(user);
            return result;
        }
        return null;
    }

    public async Task<UserDto> CreateUserAsync(RegisterDto newUser)
    {
        _passvordHelper = new();
        var result = _mapper.Map<UserEntity>(newUser);
        result.Password =  _passvordHelper.HashPasword(result.Password, out var salt);
        result.Salt = salt;
        var user = await _userRepository.CreateUserAsync(result);
        var readyUser = _mapper.Map<UserDto>(user);
        return readyUser;
    }
}
