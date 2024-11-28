using AutoMapper;
using Messages.Bll.Mappings;
using Messages.Bll.ModelsBll;
using Messages.Dal.Entityes;
using Messages.Bll.Interfaces;
using Messages.Dal.Interfaces;
using Messages.Bll.Exceptions;

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
        var user = await _userRepository.AuthenticateUserAsync(dataAuth.Nick);

        if (user is null)
        {
            throw new EntityNotFoundException("password or login does not found");
        }
        else if(_passvordHelper.VerifyPassword(dataAuth.Password, user.Password, user.Salt))
        {
            var result = _mapper.Map<UserDto>(user);
            return result;
        }
        else
        {
            throw new EntityNotFoundException("password or login does not found");
        }
    }

    public async Task<UserDto> CreateUserAsync(RegisterDto user)
    {
        var result = _mapper.Map<UserEntity>(user);
        result.Password =  _passvordHelper.HashPasword(result.Password, out var salt);
        result.Salt = salt;
        var newUser = await _userRepository.CreateUserAsync(result);
        if(newUser is null)
        {
            throw new FailedToCreateException("failed to create user");
        }
        else
        {
            var readyUser = _mapper.Map<UserDto>(newUser);
            return readyUser;
        }
    }
}
