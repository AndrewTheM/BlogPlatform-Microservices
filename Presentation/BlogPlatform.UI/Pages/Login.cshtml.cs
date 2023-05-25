using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogPlatform.UI.Pages;

public class LoginModel : PageModel
{
    public IActionResult OnGetAsync(string redirectUri)
    {
        if (HttpContext.User.Identity.IsAuthenticated)
            return LocalRedirect(redirectUri ?? "/");

        return Challenge(properties: new() { RedirectUri = redirectUri });
    }
}
