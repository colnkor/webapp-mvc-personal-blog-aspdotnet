using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace PersonalWebBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class AccountController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                ViewBag.ReturnUrl = returnUrl;
            }
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.Username == "admin" && model.Password == "S0meiPass")
            {
                var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Role, "Administrator")
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                    );

                var authProps = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProps
                    );

                if (!String.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError(string.Empty, "Invalid password or username");
            return View(model);
        }
    }
}
