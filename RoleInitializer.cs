using Site.Models;  // пространство имен модели User
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Site.Utilities;

namespace Site
{
    public class RoleInitializer
    {
        private static ILogger<RoleInitializer> _logger;
        public RoleInitializer(ILogger<RoleInitializer> logger)
        {
            _logger = logger;
        }
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            DBConfigHelper.InitializeDBConfig();
            var dbConfigDictionary = DBConfigHelper.dbConfigDictionary;
            string adminName = dbConfigDictionary["adminName"];
            string adminEmail = dbConfigDictionary["adminEmail"];
            string adminPassword = dbConfigDictionary["adminPassword"];
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                User admin = new() { Email = adminEmail, UserName = adminName };
                IdentityResult adminCreated = await userManager.CreateAsync(admin, adminPassword);
                if (adminCreated.Succeeded)
                {
                    IdentityResult adminAddedToRole = await userManager.AddToRoleAsync(admin, "admin");
                    if (!adminAddedToRole.Succeeded)
                    {
                        foreach (var error in adminAddedToRole.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in adminCreated.Errors)
                    {
                        _logger.LogError(error.Description);
                    }
                }
            }
        }
    }
}