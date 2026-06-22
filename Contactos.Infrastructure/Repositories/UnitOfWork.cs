using Contactos.Application.Contracts.Persistance;
using Contactos.Domain.Common;
using Contactos.Infrastructure.Persistance;
using System.Collections;


namespace Contactos.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly ContactsDbContext _context;
        private IUserRepository _userRepository;
        private IPostRepository _postRepository;

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public IPostRepository PostRepository => _postRepository ??= new PostRepository(_context);

        public UnitOfWork(ContactsDbContext context)
        {
            _context = context;
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if(_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }
            return (IAsyncRepository<TEntity>)_repositories[type];
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
