using Contactos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.DTOs
{
    public class AddressDto
    {
        public string? Street { get; set; }

        public string? Suite { get; set; }

        public string? City { get; set; }

        public string? ZipCode { get; set; }

        public GeoDto? Geo { get; set; }
    }
}
