using Contactos.Domain;
using Contactos.Domain.Common;

namespace Contactos.Domain
{
    public class Comment : BaseDomainModel
    {
        public string? Name { get; set; }

        public Email? Email { get; set; }

        public string? Body { get; set; }

        public int PostId { get; set; }

        public virtual Post? Post { get; set; }
    }
}
