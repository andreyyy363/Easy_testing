using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
namespace EasyTesting.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        public IActionResult Logout()
        {
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = "/"
            }, OpenIdConnectDefaults.AuthenticationScheme, "Cookies");
        }
    }

}
