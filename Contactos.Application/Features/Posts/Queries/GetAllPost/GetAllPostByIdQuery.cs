using Contactos.Application.Features.DTOs;
using Contactos.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.Posts.Queries.GetAllPost
{
    public class GetAllPostByIdQuery : IRequest<List<PostDto>>
    {
        public int UserId { get; set; }

        public GetAllPostByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
