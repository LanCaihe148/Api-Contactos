using Contactos.Application.Features.DTOs;
using Contactos.Domain;

namespace Contactos.Application.Contracts.Persistance
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<int> CountAsync(string? searchTerm = null, CancellationToken cancellationToken = default);

        Task<PaginatedResult<User>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? searchTerm = null,
            CancellationToken cancellationToken = default);
    }
}
