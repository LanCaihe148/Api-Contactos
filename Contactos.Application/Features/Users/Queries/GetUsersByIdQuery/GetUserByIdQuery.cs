using Contactos.Application.Features.DTOs;
using MediatR;


namespace Contactos.Application.Features.Users.Queries.GetUsersByIdQuery
{
    public class GetUserByIdQuery : IRequest<UserByidDto>
    {
        public int _Id { get; set; } 
        public GetUserByIdQuery(int? id)
        {
            _Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}
