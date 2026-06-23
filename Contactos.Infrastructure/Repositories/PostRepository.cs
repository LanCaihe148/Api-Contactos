using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Features.DTOs;
using Contactos.Domain;
using Contactos.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Contactos.Infrastructure.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {

        public PostRepository(ContactsDbContext context) : base(context)
        {
        }

        public async Task<int> CountAsync(string? searchTerm = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Posts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();
                query = query.Where(u => EF.Functions.ILike(u.Title!, $"%{term}%") ||
                    EF.Functions.ILike(u.Body!, $"%{term}%") || EF.Functions.ILike(u.User.Name!, $"%{term}%") || EF.Functions.ILike(u.User.UserName!, $"%{term}%"));
            }

            return await query.CountAsync(cancellationToken);
        }

        public async Task<PaginatedResult<Post>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? searchTerm = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Posts.Include(p => p.User).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();

                query = query.Where(p => EF.Functions.ILike(p.Title!,$"%{term}%") ||
                    EF.Functions.ILike(p.Body!, $"%{term}%") || EF.Functions.ILike(p.User.Name!, $"%{term}%") || EF.Functions.ILike(p.User.UserName!, $"%{term}%"));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query.OrderBy(a => a.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PaginatedResult<Post>(items,totalCount, pageIndex,pageSize);
        }
    }
}
