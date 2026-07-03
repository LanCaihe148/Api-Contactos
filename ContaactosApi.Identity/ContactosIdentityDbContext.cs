

using ContactsApi.Identity.Configurations;
using ContactsApi.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Identity
{
    public class ContactosIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ContactosIdentityDbContext(DbContextOptions<ContactosIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
