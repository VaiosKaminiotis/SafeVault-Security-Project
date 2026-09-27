using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using SafeVault.Api.Controllers;
using Xunit;

namespace SafeVault.Tests;

public class AuthorizationTests
{
    [Fact]
    public void AdminEndpoint_RequiresAdminRole()
    {
        var method = typeof(UsersController)
            .GetMethod(nameof(UsersController.GetAllUsers));

        var attribute = method!
            .GetCustomAttributes<AuthorizeAttribute>()
            .Single();

        Assert.Equal("Admin", attribute.Roles);
    }
}
