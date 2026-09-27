using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeVault.Api.Models;
using SafeVault.Api.Services;

namespace SafeVault.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly SafeHtmlService _safeHtml;

    public ProfileController(SafeHtmlService safeHtml) => _safeHtml = safeHtml;

    [HttpPost("preview")]
    public IActionResult Preview(ProfilePreviewRequest request)
    {
        var html = _safeHtml.BuildProfilePreview(
            request.DisplayName,
            request.AboutMe);

        return Content(html, "text/html");
    }
}
