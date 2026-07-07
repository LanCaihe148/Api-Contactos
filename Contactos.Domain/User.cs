using Contactos.Domain.Common;


namespace Contactos.Domain
{
    public class User : BaseDomainModel
    {
        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? WebSite { get; set; }

        public Address? Address { get; private set; }

        public Email? Email { get; private set; }

        public Phone? Phone { get; private set; }

        public Company? Company { get; private set; }

        public string? ApplicationUserId { get; set; }

        public void SetApplicationUserId(string applicationUserId)
        {
            ApplicationUserId = applicationUserId;
        }

        public virtual ICollection<Post> Posts { get; private set; } = new HashSet<Post>();

        public virtual ICollection<Album> Albums { get; private set; } = new HashSet<Album>();

        public virtual ICollection<Todo> Todos { get; private set; }= new HashSet<Todo>();
        private User() { }

        public User(string? name, string? userName, Address? address, Email? email, Phone? phone, string? webSite, Company? company)
        {
            Name = name;
            UserName = userName;
            Address = address;
            Email = email;
            Phone = phone;
            WebSite = webSite;
            Company = company;
        }
    }
}
