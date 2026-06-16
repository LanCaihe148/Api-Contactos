using Contactos.Domain.Common;

namespace Contactos.Application.Contracts.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;

        Task<int> Complete();
    }
}
