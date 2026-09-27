using System.ComponentModel.DataAnnotations;

namespace SafeVault.Api.Models;

public class RegisterRequest
{
    [Required]
    [RegularExpression("^[a-zA-Z0-9]{4,12}$")]
    public string Username { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(8), MaxLength(100)]
    public string Password { get; set; } = string.Empty;
}

public class LoginRequest
{
    [Required, MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Password { get; set; } = string.Empty;
}

public class ProfilePreviewRequest
{
    [Required, MaxLength(50)]
    public string DisplayName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string AboutMe { get; set; } = string.Empty;
}
