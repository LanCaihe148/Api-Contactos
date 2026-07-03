
using ContactsApi.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsApi.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "14711197-dd7d-4da4-bd20-05a0335d994d",
                    Email = "sandovalherest@gmail.com",
                    NormalizedEmail = "sandovalherest@gmail.com",
                    Nombre = "Efrain",
                    Apellidos = "Sandoval",
                    UserName = "e_sandoval24",
                    NormalizedUserName = "e_sandoval24",
                    PasswordHash = "AQAAAAIAAYagAAAAEL5bfd7+hnwn4SuHdUcHDsqItbpfzEU8naNfSr5CDpkcNIQ3/v4N0G1OV2siCWgSYQ==",
                    EmailConfirmed = true,
                    SecurityStamp = "220ba34e-432a-4fe6-a56a-6e0ee6ec15f2",
                    ConcurrencyStamp = "c7c7222a-6cbe-4ee9-937e-e40ff62215ad",
                },
                new ApplicationUser
                {
                    Id = "b2131d62-4757-4943-b49d-4ad397e6c468",
                    Email = "lancaihe148@gmail.com",
                    NormalizedEmail = "lancaihe148@gmail.com",
                    Nombre = "Lan",
                    Apellidos = "Caihe",
                    UserName = "lancaihe148",
                    NormalizedUserName = "lancaihe148",
                    PasswordHash = "AQAAAAIAAYagAAAAEN5qYQuOTze3G61PlSMfYAouAm8C1iLmpE5NBk1AwtTtC5d2uC1FJlHjyaaS5A2vkQ==",
                    EmailConfirmed = true,
                    SecurityStamp = "c88a0d5b-d33c-4c66-b9ce-5bb077e09afe",
                    ConcurrencyStamp = "b58af637-d112-4f4f-8269-05a0e59a4596",
                });        
        }
    }
}
