using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Features.DTOs;
using Contactos.Domain;
using Contactos.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ContactsDbContext context) : base(context)
        {
        }

        public async Task<int> CountAsync(string? searchTerm = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Users.AsQueryable(); 

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();
                query = query.Where(u => EF.Functions.ILike(u.Name!, $"%{term}%") ||
                                         EF.Functions.ILike(u.Email.Direction!, $"%{term}%"));
            }

            return await query.CountAsync(cancellationToken);
        }

        public async Task<PaginatedResult<User>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? searchTerm = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();
                query = query.Where(u => EF.Functions.ILike(u.Name!, $"%{term}%") ||
                                         EF.Functions.ILike(u.Email.Direction!, $"%{term}%"));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(u => u.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<User>(items, totalCount, pageIndex, pageSize);
        }
    }
}
