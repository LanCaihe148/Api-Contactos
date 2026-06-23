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

            await SeedPostsAsync(context);
        }

        private static async Task SeedPostsAsync(ContactsDbContext context)
        {
            if (await context.Posts!.AnyAsync())
                return;

            // Trae los Ids de los usuarios que ya existen
            var userIds = await context.Users!
                .Select(u => u.Id)
                .ToListAsync();

            if (!userIds.Any())
                return;

            var postFaker = new Faker<Post>()
                .RuleFor(p => p.Title, f => f.Lorem.Sentence(6))
                .RuleFor(p => p.Body, f => f.Lorem.Paragraphs(2))
                .RuleFor(p => p.UserId, f => f.PickRandom(userIds));

            var posts = postFaker.Generate(100); // 100 posts repartidos entre los 40 usuarios

            context.Posts!.AddRange(posts);
            await context.SaveChangesAsync();
        }
    }
}
