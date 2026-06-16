using Contactos.Application.Contracts.Persistance;
using Contactos.Domain;
using Contactos.Infrastructure.Persistance;
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
    }
}
