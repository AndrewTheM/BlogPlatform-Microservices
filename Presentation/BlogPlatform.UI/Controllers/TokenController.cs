using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.UI.Controllers
{
    [Authorize]
    [Route("token")]
    public class TokenController : Controller
    {
        [Route("access")]
        public async Task<IActionResult> Access()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return Content(accessToken);
        }
        
        [Route("id")]
        public async Task<IActionResult> Id()
        {
            var accessToken = await HttpContext.GetTokenAsync("id_token");
            return Content(accessToken);
        }
        
        [Route("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var accessToken = await HttpContext.GetTokenAsync("refresh_token");
            return Content(accessToken);
        }
    }
}
