using Contactos.Application.Features.DTOs;
using MediatR;


namespace Contactos.Application.Features.Users.Commands.CreateUsersCommand
{
    public class CreateUserCommand : IRequest<int>
    {
        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? WebSite { get; set; }

        public AddressDto? Address { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public CompanyDto? Company { get; set; }
    }
}
