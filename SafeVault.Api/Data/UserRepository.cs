using Microsoft.EntityFrameworkCore;
using SafeVault.Api.Models;

namespace SafeVault.Api.Data;

public class UserRepository
{
    private readonly SafeVaultDbContext _db;

    public UserRepository(SafeVaultDbContext db) => _db = db;

    public Task<AppUser?> FindByUsernameAsync(string username) =>
        _db.Users.SingleOrDefaultAsync(u => u.Username == username);

    public Task<AppUser?> FindByUsernameWithSqlAsync(string username) =>
        _db.Users
           .FromSqlInterpolated($"SELECT * FROM Users WHERE Username = {username}")
           .SingleOrDefaultAsync();

    public Task<List<AppUser>> GetAllAsync() =>
        _db.Users.AsNoTracking().OrderBy(u => u.Username).ToListAsync();

    public async Task AddAsync(AppUser user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }
}
