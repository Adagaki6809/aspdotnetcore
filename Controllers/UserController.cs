using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Models;
using Site.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Controllers
{
    [Authorize]
    public class UserController: Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _appEnvironment;
        public UserController(UserManager<User> userManager, IWebHostEnvironment appEnvironment)
        {
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }
        
        public async Task<IActionResult> Index() => View(await _userManager.FindByNameAsync(User.Identity.Name));

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            EditUserViewModel model = new() { Id = user.Id, Name = user.UserName, Email = user.Email, Year = user.Year };
            return View(model);
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.UserName = model.Name;
                    user.Email = model.Email;
                    user.Year = model.Year;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UploadAvatar(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            AvatarViewModel model = new() { PathToAvatar = user.PathToAvatar };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile uploadedFile, string id)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string pathToAvatar = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathToAvatar, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                User user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.PathToAvatar = pathToAvatar;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SignOut([FromServices] SignInManager<User> signInManager)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
