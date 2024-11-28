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
        return user;
    }
}
