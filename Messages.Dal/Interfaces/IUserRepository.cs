
using Messages.Dal.Entityes;

namespace Messages.Dal.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> AuthenticateUserAsync(string nick);

        Task<UserEntity> CreateUserAsync(UserEntity user);
    }
}
