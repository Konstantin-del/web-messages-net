using Microsoft.EntityFrameworkCore;
using Messages.Dal.Dtos;

namespace Messages.Dal
{
    public class Context : DbContext
    {
        public DbSet<UserDto> Users { get; set; }

        public DbSet<ContactDto> Contacts { get; set; }

        public DbSet<MessageDto> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=fred;Database=MessagesTest;";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
