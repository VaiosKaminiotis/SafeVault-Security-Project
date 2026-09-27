using System.ComponentModel.DataAnnotations;
using SafeVault.Api.Models;
using Xunit;

namespace SafeVault.Tests;

public class InputValidationTests
{
    private static bool IsValid(object model)
    {
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        return Validator.TryValidateObject(
            model,
            context,
            results,
            validateAllProperties: true);
    }

    [Fact]
    public void ValidRegistration_Passes()
    {
        var model = new RegisterRequest
        {
            Username = "User123",
            Email = "user@example.com",
            Password = "StrongPass123!"
        };

        Assert.True(IsValid(model));
    }

    [Theory]
    [InlineData("ab")]
    [InlineData("user_name")]
    [InlineData("<script>")]
    public void InvalidUsername_Fails(string username)
    {
        var model = new RegisterRequest
        {
            Username = username,
            Email = "user@example.com",
            Password = "StrongPass123!"
        };

        Assert.False(IsValid(model));
    }
}
