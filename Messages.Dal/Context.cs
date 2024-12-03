using Microsoft.EntityFrameworkCore;
using Messages.Dal.Entityes;

namespace Messages.Dal;

public class Context : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<ContactEntity> Contacts { get; set; }

    public DbSet<MessageDto> Messages { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    { }
}
