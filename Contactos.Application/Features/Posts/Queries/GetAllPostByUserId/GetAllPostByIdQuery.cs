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
    public class GetAllPostByIdQuery : IRequest<PaginatedResult<PostDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int UserId { get; set; }

        public GetAllPostByIdQuery()
        {
        }

        public GetAllPostByIdQuery(int pageIndex, int pageSize, int userId)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            
            UserId = userId;
        }


        //public GetAllPostByIdQuery(int userId)
        //{
        //    UserId = userId;
        //}


    }
}
