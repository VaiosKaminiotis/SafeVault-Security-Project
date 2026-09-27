using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SafeVault.Api.Data;
using SafeVault.Api.Models;
using SafeVault.Api.Services;

namespace SafeVault.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserRepository _users;
    private readonly TokenService _tokens;
    private readonly PasswordHasher<AppUser> _hasher = new();

    public AuthController(UserRepository users, TokenService tokens)
    {
        _users = users;
        _tokens = tokens;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (await _users.FindByUsernameAsync(request.Username) is not null)
            return Conflict(new { message = "Username is already in use." });

        var user = new AppUser
        {
            Username = request.Username,
            Email = request.Email,
            Role = "User"
        };

        user.PasswordHash = _hasher.HashPassword(user, request.Password);
        await _users.AddAsync(user);

        return Created("", new { user.Id, user.Username, user.Email, user.Role });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _users.FindByUsernameAsync(request.Username);

        if (user is null)
            return Unauthorized(new { message = "Invalid credentials." });

        var result = _hasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (result == PasswordVerificationResult.Failed)
            return Unauthorized(new { message = "Invalid credentials." });

        return Ok(new { token = _tokens.CreateToken(user) });
    }
}
