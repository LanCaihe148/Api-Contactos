using Contactos.Domain;

namespace Contactos.Application.Contracts.Persistance
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
