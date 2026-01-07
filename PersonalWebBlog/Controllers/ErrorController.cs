using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWebBlog.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/error")]
        public IActionResult Index()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var path = exceptionFeature?.Path ?? "";

            if (path.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
            {
                return View("AdminError");
            }

            return View("UserError");
        }
    }
}
