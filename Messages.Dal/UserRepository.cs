using Messages.Dal.Dtos;

namespace Messages.Dal
{
    public class UserRepository
    { 
        private Context _context = new();
        public UserDto UserAuth(string nick)
        { 
            var authorizeUser = _context.Users.Where(s => s.Nick == nick).FirstOrDefault();
            return authorizeUser;
        }

        public UserDto CreateUser(UserDto user)
        {    
            _context.Users.Add(user);
            _context.SaveChanges();
            Console.WriteLine(user.Id);
            return user;
        }
    }
}
