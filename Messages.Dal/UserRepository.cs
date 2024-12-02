using Messages.Dal.Entityes;
using Messages.Dal.Interfaces;

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
       return _context.Users.Where(s => s.Nick == nick).FirstOrDefault();
    }

    public async Task<UserEntity> CreateUserAsync(UserEntity user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var result  = _context.Users.FirstOrDefault(n => n.Nick == user.Nick);
        return result;
    }

    public async Task<UpdateUserEntity> UpdateUserAsync(Guid id, UpdateUserEntity userName)
    {
        var user = _context.Users.FirstOrDefault(n=>n.Id == id);
        user.Name = userName.Name;
        await _context.SaveChangesAsync();
        UpdateUserEntity newUserName = new();
        newUserName = _context.Users.FirstOrDefault(n => n.Id == id).Name == userName.Name ? userName : null;
        return newUserName;
    }
}
