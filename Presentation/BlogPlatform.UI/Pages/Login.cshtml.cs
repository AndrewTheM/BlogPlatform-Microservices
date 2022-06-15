using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogPlatform.UI.Pages;

public class LoginModel : PageModel
{
    public async Task OnGetAsync(string redirectUri)
    {
        await HttpContext.ChallengeAsync(
            OpenIdConnectDefaults.AuthenticationScheme,
            properties: new() { RedirectUri = redirectUri });
    }
}
