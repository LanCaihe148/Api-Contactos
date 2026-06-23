using Contactos.Application.Features.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.Users.Queries.GetUsersByString
{
    public class GetUserByStringQuery : IRequest<PaginatedResult<UserByidDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; } 
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;

        public GetUserByStringQuery(int pageIndex, int pageSize, string? searchTerm)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SearchTerm = searchTerm;

        }
    }
}
