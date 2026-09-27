using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeVault.Api.Data;

namespace SafeVault.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly UserRepository _users;

    public UsersController(UserRepository users) => _users = users;

    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            username = User.Identity?.Name,
            role = User.FindFirstValue(ClaimTypes.Role)
        });
    }

    [HttpGet("admin/all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _users.GetAllAsync();

        return Ok(users.Select(u => new
        {
            u.Id,
            u.Username,
            u.Email,
            u.Role
        }));
    }
}
