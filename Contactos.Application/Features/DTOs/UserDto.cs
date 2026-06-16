using Contactos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.DTOs
{
    public class UserDto
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? CompanyName { get; set; }

        public string? City { get; set; }
    }
}
