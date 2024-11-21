using Messages.Core.Models.Requests;
using Messages.Core.Models.Responses;
using Messages.Core.Dtos;
using Messages.Dal;

namespace Messages.Bll
{
    public class UserService
    {
        private UserRepository? _userRepository { get; set; }
        private PasswordHelper? _passvordHelper { get; set; }

        public UserResponse UserAuth(AuthUserRequest dataAuth)
        {
            _passvordHelper = new();
            _userRepository = new();
            var data = _userRepository.UserAuth(dataAuth.Nick);
            
            if (data == null)
            { 
                return null;
            }
            else
            {
                UserResponse objAuth = new();
                _passvordHelper.VerifyPassword(dataAuth.Password, data.Password, data.Salt);
                objAuth.Id = data.Id;
                objAuth.Nick = data.Nick;
                objAuth.Name = data.Name;
                return objAuth;
            }
        }

        public UserResponse CreateUser(RegistrationUserRequest user)
        {
            _userRepository = new();
            _passvordHelper = new();
            UserDto userDto = new(); 
            userDto.Password = _passvordHelper.HashPasword(user.Password, out var salt);
            userDto.Salt = salt;
            userDto.Name = user.Name;
            userDto.Nick = user.Nick;
            userDto.RegistrationDate = user.RegistrationDate;

            var newUser = _userRepository.CreateUser(userDto);

            UserResponse response = new();
            response.Id = newUser.Id;
            response.Name = newUser.Name;
            response.Nick = newUser.Nick;
            return response;
        }
    }
}
