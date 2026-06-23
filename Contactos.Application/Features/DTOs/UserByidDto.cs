

namespace Contactos.Application.Features.DTOs
{
    public class UserByidDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? WebSite { get; set; }

        public AddressDto? Address { get; set; }

        public EmailDto? Email { get;  set; }

        public PhoneDto? Phone { get; set; }

        public CompanyDto? Company { get; set; }
    }
}
