using Microsoft.EntityFrameworkCore;
using Messages.Dal.Entityes;

namespace Messages.Dal
{
    public class Context : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<ContactEntity> Contacts { get; set; }

        public DbSet<MessageDto> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=fred;Database=MessagesTest;";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
