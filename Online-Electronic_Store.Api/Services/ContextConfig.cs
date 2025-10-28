using DAL.Data.Context;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Services
{
    public class ContextConfig
    {
        public static async Task SeedDataAsync(
            AppDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager, 
            IConfiguration configuration)
        {
            await SeedRolesAsync(roleManager);
            await SeedAdminUserAsync(userManager, configuration);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)  
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));  

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole<Guid>("User"));  
        }

        private static async Task SeedAdminUserAsync(UserManager<AppUser> userManager, IConfiguration configuration)
        {
 
            var adminEmail = configuration["AdminSettings:Email"];
            var adminPassword = configuration["AdminSettings:Password"];

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var admin = new AppUser
                {
                    Id = Guid.NewGuid(),
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
