using Contactos.Application.Features.DTOs;
using MediatR;

namespace Contactos.Application.Features.Posts.Queries.GetAllPost
{
    public class GetAllPostByIdQuery : IRequest<PaginatedResult<PostDto>>
    {
        public int UserId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        

        public GetAllPostByIdQuery()
        {
        }

        public GetAllPostByIdQuery(int userId, int pageIndex, int pageSize )
        {
            UserId = userId;
            PageIndex = pageIndex < 1 ? 1 : pageIndex;
            PageSize = pageSize < 1 ? 10 : pageSize;
            
        }


        //public GetAllPostByIdQuery(int userId)
        //{
        //    UserId = userId;
        //}


    }
}
