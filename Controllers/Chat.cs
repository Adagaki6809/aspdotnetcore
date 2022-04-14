using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Controllers
{
    [Authorize]
    public class Chat: Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
