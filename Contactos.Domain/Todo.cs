using Contactos.Domain.Common;

namespace Contactos.Domain
{
    public class Todo : BaseDomainModel
    {
        public string Title { get; set; }
        public bool Completed { get; set; } 
        public int UserId { get; set; }

        public User? User { get; set; }
   
    }
}
