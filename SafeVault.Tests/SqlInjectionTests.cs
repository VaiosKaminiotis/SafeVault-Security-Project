using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SafeVault.Api.Data;
using SafeVault.Api.Models;
using Xunit;

namespace SafeVault.Tests;

public class SqlInjectionTests
{
    [Fact]
    public async Task InjectionPayload_IsTreatedAsData()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<SafeVaultDbContext>()
            .UseSqlite(connection)
            .Options;

        await using var db = new SafeVaultDbContext(options);
        await db.Database.EnsureCreatedAsync();

        db.Users.Add(new AppUser
        {
            Username = "admin",
            Email = "admin@example.com",
            PasswordHash = "test",
            Role = "Admin"
        });

        await db.SaveChangesAsync();

        var repository = new UserRepository(db);
        var result = await repository.FindByUsernameWithSqlAsync("' OR 1=1 --");

        Assert.Null(result);
    }
}
