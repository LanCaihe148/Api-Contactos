using Bogus;
using Contactos.Domain;
using Contactos.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


namespace Contactos.Infrastructure
{
    public static class SeedData
    {
        public static async Task SeedAsync(ContactsDbContext context)
        {
            //if (await context.Users!.AnyAsync())
            //    return;

            var geoFaker = new Faker<Geo>()
                .CustomInstantiator(f => new Geo(
                    f.Address.Latitude().ToString(),
                    f.Address.Longitude().ToString()
                ));

            var addressFaker = new Faker<Address>()
                .CustomInstantiator(f => new Address(
                    f.Address.StreetName(),
                    f.Random.Bool() ? $"Suite {f.Random.Number(1, 500)}" : "",
                    f.Address.City(),
                    f.Address.ZipCode(),
                    geoFaker.Generate()
                ));

            var companyFaker = new Faker<Company>()
                .CustomInstantiator(f => new Company(
                    f.Company.CompanyName(),
                    f.Company.CatchPhrase(),
                    f.Company.Bs()
                ));

            var userFaker = new Faker<User>()
                .CustomInstantiator(f => new User(
                    f.Name.FullName(),
                    f.Internet.UserName(),
                    addressFaker.Generate(),
                    new Email(f.Internet.Email()),
                    new Phone(f.Phone.PhoneNumber()),
                    f.Internet.Url(),
                    companyFaker.Generate()
                ));

            var users = userFaker.Generate(40);

            context.Users!.AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}
