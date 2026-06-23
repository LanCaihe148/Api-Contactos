using Contactos.Domain.Common;


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
