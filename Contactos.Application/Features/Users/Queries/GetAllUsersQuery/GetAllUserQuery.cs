using Contactos.Application.Features.DTOs;
using MediatR;


namespace Contactos.Application.Features.Users.Queries.GetAllUsersQuery
{
    public class GetAllUserQuery : IRequest<PaginatedResult<UserByidDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; } 
        public string? SortBy { get; set; } 
        public bool SortDescending { get; set; } = false;
        public GetAllUserQuery()
        {
        }

        public GetAllUserQuery(int pageIndex, int pageSize, string? searchTerm = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SearchTerm = searchTerm;
        }
        
    }
}
