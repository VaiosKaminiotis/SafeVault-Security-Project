using Microsoft.EntityFrameworkCore;
using SafeVault.Api.Models;

namespace SafeVault.Api.Data;

public class SafeVaultDbContext : DbContext
{
    public SafeVaultDbContext(DbContextOptions<SafeVaultDbContext> options) : base(options) { }

    public DbSet<AppUser> Users => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<AppUser>().HasIndex(u => u.Email).IsUnique();
    }
}
