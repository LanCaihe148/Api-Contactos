using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Xml.Linq;

namespace ContactsApi.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "869ca953-7bfa-44a6-8fbc-6d9f722012a5",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",


                },
                
                new IdentityRole { 
                    
                    Id = "4746e1a8-fdc3-4e37-9609-f201df7cdfa5",
                    Name = "Operator",
                    NormalizedName = "OPERATOR",
                });
        }
    }
}
