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
                query = query.Where(u => u.Title.Contains(searchTerm) ||
                                         u.Body.Contains(searchTerm) || u.User.Name.Contains(searchTerm));
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
                query = query.Where(p => p.Title.Contains(searchTerm) ||
                    p.Body.Contains(searchTerm) || p.User.Name.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query.OrderBy(a => a.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PaginatedResult<Post>(items,totalCount, pageIndex,pageSize);
        }
    }
}
