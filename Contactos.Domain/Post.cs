using Contactos.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Domain
{
    public class Post : BaseDomainModel
    {
        public string? Title { get; set; }

        public string? Body { get; set; }

        public int UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    }
}
