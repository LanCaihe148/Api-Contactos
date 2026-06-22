using Contactos.Application.Features.DTOs;
using Contactos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Contracts.Persistance
{
    public interface IPostRepository : IAsyncRepository<Post>
    {
        Task<int> CountAsync(string? searchTerm = null, CancellationToken cancellationToken = default);

        Task<PaginatedResult<Post>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? searchTerm = null,
            CancellationToken cancellationToken = default);
    }
}
