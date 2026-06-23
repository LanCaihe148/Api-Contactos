using Contactos.Application.Features.DTOs;
using MediatR;


namespace Contactos.Application.Features.Posts.Queries.GetAllPost
{
    public class GetAllPostQuery : IRequest<PaginatedResult<PostDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;

        public GetAllPostQuery() 
        {    
        }

        public GetAllPostQuery(int pageIndex, int pageSize, string? searchTerm)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SearchTerm = searchTerm;
        }
    }
}
