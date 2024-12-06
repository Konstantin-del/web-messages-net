using Messages.Dal.Entityes;
using Messages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messages.Dal;

public class UserRepository(Context context) : IUserRepository
{
    public void CreateDB()
    {
        context.Database.EnsureCreated();
    }

    public async Task<UserEntity> AuthenticateUserAsync(string nick)
    {
       return await context.Users.FirstOrDefaultAsync(s => s.Nick == nick);
    }

    public async Task<UserEntity> CreateUserAsync(UserEntity user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        var result = await context.Users.FirstOrDefaultAsync(n => n.Nick == user.Nick);
        return result;
    }

    public async Task<UserEntity> GetUserByIdAsync(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<UserEntity> GetUserByNickAsync(string nick)
    {
        return await context.Users.FirstOrDefaultAsync(n => n.Nick == nick);
    }

    public async Task<UserEntity> UpdateUserAsync(Guid id, UpdateUserEntity userName)
    {
        var user = await context.Users.FirstOrDefaultAsync(n => n.Id == id);
        user.Name = userName.Name;
        await context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await context.Users.Where(n => n.Id == id).ExecuteDeleteAsync();
        await context.SaveChangesAsync();
    }
}
