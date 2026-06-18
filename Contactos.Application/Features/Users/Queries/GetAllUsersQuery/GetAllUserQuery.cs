using Contactos.Application.Features.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.Users.Queries.GetAllUsersQuery
{
    public class GetAllUserQuery : IRequest<List<UserByidDto>>
    {
        //public int PageNumber { get; set; } = 1;
        //public int PageSize { get; set; } = 10;
        public GetAllUserQuery()
        {
        }
    }
}
