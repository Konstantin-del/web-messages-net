
using Messages.Dal.Entityes;

namespace Messages.Dal.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> AuthenticateUserAsync(string nick);

    Task <string> CreateUserAsync(UserEntity user);

    Task<UserEntity> UpdateUserAsync(Guid id, UpdateUserEntity userName);

    Task DeleteUserAsync(Guid id);

    Task<UserEntity> GetUserByIdAsync(Guid id);

    Task<UserEntity> GetUserByNickAsync(string nick);
}
