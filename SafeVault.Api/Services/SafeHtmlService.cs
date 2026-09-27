using System.Text.Encodings.Web;

namespace SafeVault.Api.Services;

public class SafeHtmlService
{
    public string BuildProfilePreview(string displayName, string aboutMe)
    {
        var safeName = HtmlEncoder.Default.Encode(displayName);
        var safeAbout = HtmlEncoder.Default.Encode(aboutMe);

        return $"<h2>{safeName}</h2><p>{safeAbout}</p>";
    }
}
