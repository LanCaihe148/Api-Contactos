using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Domain.Common
{
    public abstract class BaseDomainModel
    {
        public int Id { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset? LastModifiedDate { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
