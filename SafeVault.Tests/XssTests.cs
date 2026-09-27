using SafeVault.Api.Services;
using Xunit;

namespace SafeVault.Tests;

public class XssTests
{
    [Fact]
    public void ScriptTags_AreEncoded()
    {
        var service = new SafeHtmlService();

        var html = service.BuildProfilePreview(
            "Alice",
            "<script>alert('xss')</script>");

        Assert.DoesNotContain("<script>", html);
        Assert.Contains("&lt;script&gt;", html);
    }
}
