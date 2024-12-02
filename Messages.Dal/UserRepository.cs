using Messages.Dal.Entityes;
using Messages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messages.Dal;

public class UserRepository : IUserRepository
{
    Context _context;
    public UserRepository()
    {
        _context = new();
    }

    public async Task<UserEntity> AuthenticateUserAsync(string nick)
    {
       return _context.Users.FirstOrDefault(s => s.Nick == nick);
    }

    public async Task<UserEntity> CreateUserAsync(UserEntity user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var result  = _context.Users.FirstOrDefault(n => n.Nick == user.Nick);
        return result;
    }

    public async Task<UserEntity> GetUserByIdAsync(Guid id) => _context.Users.FirstOrDefault(n => n.Id == id);

    public async Task<UpdateUserEntity> UpdateUserAsync(Guid id, UpdateUserEntity userName)
    {
        var user = _context.Users.FirstOrDefault(n=>n.Id == id);
        user.Name = userName.Name;
        await _context.SaveChangesAsync();
        UpdateUserEntity newUserName = new();
        newUserName.Name = user.Name;
        return newUserName;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _context.Users.Where(n => n.Id == id).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }
}
