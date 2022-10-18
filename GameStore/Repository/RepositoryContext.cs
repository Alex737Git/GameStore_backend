using System.Drawing;
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
        // modelBuilder.Entity<Category>().Property(t => t.Id).HasColumnName("GenreId");
        // modelBuilder.Entity<Game>().Property(t => t.Id).HasColumnName("GameId");
      // modelBuilder.Entity<Photo>().Property(t => t.Id).HasColumnName("PhotoId");
        
        // modelBuilder.Entity<Category>().Property(g => g.Body).HasMaxLength(1024);
        // modelBuilder.Entity<Category>().Property(g => g.Name).HasMaxLength(30).IsRequired();
        
        modelBuilder.Entity<Game>().Property(g => g.Title).HasMaxLength(500).IsRequired();
        modelBuilder.Entity<Game>().Property(g => g.Price).HasPrecision(2).IsRequired();
        modelBuilder.Entity<Game>().Property(g => g.Body).HasMaxLength(2000).IsRequired();
        
        
        modelBuilder
            .Entity<User>()
            .HasMany<Game>(e => e.Games)
            .WithOne(e => e.User)
            .OnDelete(DeleteBehavior.Cascade);
        


        modelBuilder.ApplyConfiguration(new RoleConfiguration());
         // modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        // modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        // modelBuilder.ApplyConfiguration(new GameConfiguration());
        
    }

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Game>? Games { get; set; }
}