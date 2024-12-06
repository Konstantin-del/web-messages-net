using Microsoft.EntityFrameworkCore;
using Messages.Dal.Entityes;

namespace Messages.Dal;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    { }
    
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<ContactEntity> Contacts { get; set; }

    public DbSet<MessageEntity> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasIndex(s => s.Nick)
            .IsUnique();
        modelBuilder.Entity<ContactEntity>()
            .Property(s => s.NameContact)
            .IsRequired()
            .HasMaxLength(20);
        modelBuilder.Entity<ContactEntity>()
            .HasKey(s => new { s.OwnerId, s.RecipiendId });
        modelBuilder.Entity<MessageEntity>()
            .Property(s => s.Message)
            .IsRequired();
        modelBuilder.Entity<MessageEntity>()
            .Property(s => s.IsDelivered)
            .HasDefaultValue(false);
    }

}
