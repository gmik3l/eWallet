using Microsoft.EntityFrameworkCore;
using test.DbModels;

namespace test;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(o => o.Id);
        
        modelBuilder.Entity<User>().HasIndex(o => o.Id);

        modelBuilder.Entity<Account>().HasKey(o => o.Id);

        modelBuilder.Entity<Account>().HasIndex(o => o.Id);

        modelBuilder.Entity<User>()
            .HasMany(o => o.Accounts)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
}