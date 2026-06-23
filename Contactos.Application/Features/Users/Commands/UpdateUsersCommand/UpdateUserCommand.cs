using Contactos.Application.Features.DTOs;
using MediatR;

namespace Contactos.Application.Features.Users.Commands.UpdateUsersCommand
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? WebSite { get; set; }

        public AddressDto? Address { get; set; }

        public EmailDto? Email { get; set; }

        public PhoneDto? Phone { get; set; }

        public CompanyDto? Company { get; set; }
    }
}
