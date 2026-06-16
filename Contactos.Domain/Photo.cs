using Contactos.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Domain
{
    public class Photo : BaseDomainModel
    {
        public string? Title { get; set; }

        public string? Url { get; set; }

        public int AlbumId { get; set; }

        public string? ThumbnailUrl { get; set; }

        public virtual Album? Album { get; set; }

        

    }
}
