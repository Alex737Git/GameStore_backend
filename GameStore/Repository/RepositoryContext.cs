using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository;

public class RepositoryContext : IdentityDbContext<User> 
{
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
       
        
        modelBuilder.Entity<Game>().Property(g => g.Title).HasMaxLength(500).IsRequired();
        modelBuilder.Entity<Game>().Property(g => g.Price).HasPrecision(2).IsRequired();
        modelBuilder.Entity<Game>().Property(g => g.Body).HasMaxLength(2000).IsRequired();
        
        
        modelBuilder
            .Entity<User>()
            .HasMany<Game>(e => e.Games)
            .WithOne(e => e.User)
            .OnDelete(DeleteBehavior.Cascade);
        


        modelBuilder.ApplyConfiguration(new RoleConfiguration());
       
        
    }

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Game>? Games { get; set; }
    public DbSet<Comment>? Comments { get; set; }
}