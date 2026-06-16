using Contactos.Application.Features.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.Users.Queries.GetAllUsersQuery
{
    public class GetAllUserQuery : IRequest<List<UserDto>>
    {
        public GetAllUserQuery()
        {
        }
    }
}
