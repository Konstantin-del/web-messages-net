
using Messages.Dal.Entityes;

namespace Messages.Dal.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> AuthenticateUserAsync(string nick);

    Task<UserEntity> CreateUserAsync(UserEntity user);

    Task<UpdateUserEntity> UpdateUserAsync(Guid id, UpdateUserEntity userName);

    Task DeleteUserAsync(Guid id);

    Task<UserEntity> GetUserByIdAsync(Guid id);
}
