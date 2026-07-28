using Microsoft.AspNetCore.Identity;

namespace PersonalPortfolio.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Add custom properties if needed
        public string DisplayName { get; set; } = string.Empty;
    }
}