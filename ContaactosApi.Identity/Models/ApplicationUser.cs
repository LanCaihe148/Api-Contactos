using Microsoft.AspNetCore.Identity;

namespace ContactsApi.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
    }
}
