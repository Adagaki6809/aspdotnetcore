using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IWebHostEnvironment _appEnvinronment;
        public ErrorController(IWebHostEnvironment appEnvinronment)
        {
            _appEnvinronment = appEnvinronment;
        }
        public IActionResult PageNotFound()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            ViewData["NotFoundPath"] = originalPath;
            return View();
        }
    }
}