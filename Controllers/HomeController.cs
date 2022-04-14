using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var context = HttpContext;
            foreach (var c in context.Request.Cookies)
                _logger.LogInformation($"{c.Key} - {c.Value}");

            //foreach (var elem in Request.Form)
            //    _logger.LogInformation("FORM " + elem.Key + " " + elem.Value);
            foreach (var elem in RouteData.Values)
                _logger.LogInformation("ROUTE " + elem.Key + " " + elem.Value);
            foreach (var elem in Request.Query)
                _logger.LogInformation("QUERY " + elem.Key + " " + elem.Value);


            return View();
        }
        [Authorize(Roles = "moderator")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
