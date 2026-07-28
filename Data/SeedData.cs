using Microsoft.AspNetCore.Identity;
using PersonalPortfolio.Models;

namespace PersonalPortfolio.Data
{
    public static class SeedData
    {
        /// <summary>
        /// Reads the admin password from environment variables.
        /// Falls back to a development-only default if not set.
        /// In production, ALWAYS set the ADMIN_PASSWORD environment variable.
        /// </summary>
        private static string GetAdminPassword()
        {
            var password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
            if (string.IsNullOrWhiteSpace(password))
            {
                // Development-only fallback — never use this in production
                password = "Admin@123";
            }
            return password;
        }

        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create Admin role if it doesn't exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Create admin user if it doesn't exist; ensure role if it does
            var adminEmail = "admin@husamghannam.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    DisplayName = "Husam Ghannam"
                };
                
                var result = await userManager.CreateAsync(adminUser, GetAdminPassword());
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to seed admin user: {errors}");
                }
            }
            else
            {
                // Ensure admin role is assigned (do NOT reset password — allow user to change it permanently)
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}