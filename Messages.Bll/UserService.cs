using Messages.Core.Models.Requests;
using Messages.Core.Models.Responses;
using Messages.Dal;

namespace Messages.Bll
{
    public class UserService
    {
        public UserResponse GetUser(AuthUserRequest authData)
        {
            UserResponse user = new();
            user.Id = new Guid();
            user.Name = "Joe";
            user.Nick = "j";

            return user;
        }
    }
}
