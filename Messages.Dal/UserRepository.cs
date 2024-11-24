using Messages.Dal.Dtos;

namespace Messages.Dal
{
    public class UserRepository
    {
        Context _context;
        public UserRepository()
        {
            _context = new();
        }

        public UserDto UserAuth(string nick)
        {
            var authorizeUser = _context.Users.Where(s => s.Nick == nick).FirstOrDefault();
            return authorizeUser;
        }

        public UserDto CreateUser(UserDto user)
        {    
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
