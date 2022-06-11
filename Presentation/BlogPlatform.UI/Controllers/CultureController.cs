using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.UI.Controllers;

[Route("culture")]
public class CultureController : Controller
{
    [Route("set")]
    public ActionResult SetCulture(string culture, string redirectUri)
    {
        if (culture is not null)
        {
            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(culture)
            ));
        }

        return LocalRedirect(redirectUri ?? "/");
    }
}
