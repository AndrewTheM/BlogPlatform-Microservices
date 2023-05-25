using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("token")]
    public class TokenController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return Content(accessToken);
        }
    }
}
