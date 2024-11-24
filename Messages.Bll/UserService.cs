using AutoMapper;
using Messages.Bll.Mappings;
using Messages.Bll.ModelsBll;
using Messages.Dal.Dtos;
using Messages.Dal;

namespace Messages.Bll
{
    public class UserService
    {
        UserRepository _userRepository;
        PasswordHelper _passvordHelper;
        Mapper _mapper;

        public UserService()
        {
            _userRepository = new();
            _passvordHelper = new();

            var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new UserMapperProfileBll());
            });

            _mapper = new Mapper(config);
        }

        public UserBll UserAuth(AuthBll dataAuth)
        {
            _passvordHelper = new();
           
            var user = _userRepository.UserAuth(dataAuth.Nick);

            if (user is null)
            { 
                return null;
            }
            if(_passvordHelper.VerifyPassword(dataAuth.Password, user.Password, user.Salt))
            {
                var result = _mapper.Map<UserBll>(user);
                return result;
            }
            return null;
        }

        public UserBll CreateUser(RegisterBll newUser)
        {
            _passvordHelper = new();
            var result = _mapper.Map<UserDto>(newUser);
            result.Password = _passvordHelper.HashPasword(result.Password, out var salt);
            result.Salt = salt;
            _userRepository = new();
            var user = _userRepository.CreateUser(result);
            var readyUser = _mapper.Map<UserBll>(user);
            return readyUser;
        }
    }
}
