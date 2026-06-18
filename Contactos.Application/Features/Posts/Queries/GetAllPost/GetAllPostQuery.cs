using Contactos.Application.Features.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.Posts.Queries.GetAllPost
{
    public class GetAllPostQuery : IRequest<List<PostDto>>
    {
        public GetAllPostQuery() 
        {    
        }
    }
}
