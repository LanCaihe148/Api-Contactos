using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApi.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "869ca953-7bfa-44a6-8fbc-6d9f722012a5",
                    UserId = "14711197-dd7d-4da4-bd20-05a0335d994d"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "4746e1a8-fdc3-4e37-9609-f201df7cdfa5",
                    UserId = "b2131d62-4757-4943-b49d-4ad397e6c468"
                });
        }
    }
}
