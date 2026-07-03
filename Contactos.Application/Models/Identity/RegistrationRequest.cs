using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Models.Identity
{
    public class RegistrationRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
