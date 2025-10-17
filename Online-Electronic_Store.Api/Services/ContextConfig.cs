
using DAL.Data.Context;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Services
{
    public class ContextConfig
    {
        private static readonly string SeedAdminEmail = "Abdullah@gmail.com";
        public static async Task SeedDataAsync(AppDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await SeedUserAsync(userManager, roleManager);
        }

        private static async Task SeedUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Exists Role 
           if(!await roleManager.RoleExistsAsync("Admin"))
           {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
           }
           if (!await roleManager.RoleExistsAsync("User"))
           {
                await roleManager.CreateAsync(new IdentityRole("User"));
           }
            var AdminEmail = SeedAdminEmail;
            var AdminUser= await userManager.FindByEmailAsync(AdminEmail);
            if (AdminUser == null)
            {
                var id = Guid.NewGuid().ToString();
                AdminUser = new AppUser
                {
                    Id = id,
                    UserName = AdminEmail,
                    Email = AdminEmail,

                };
                var resulet= await userManager.CreateAsync(AdminUser,"Abdullah304106@");
                await userManager.AddToRoleAsync(AdminUser, "Admin");


            }

        }
    }
}
