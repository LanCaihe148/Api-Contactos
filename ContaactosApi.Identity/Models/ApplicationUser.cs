using Microsoft.AspNetCore.Identity;

namespace ContactsApi.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public int? UserId { get; set; } 
    }
}
