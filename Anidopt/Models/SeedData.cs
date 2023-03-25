using Anidopt.Data;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AnidoptContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<AnidoptContext>>()))
        {
            // Look for any movies.
            if (context.Animal.Any())
            {
                return;   // DB has been seeded
            }
            context.Animal.AddRange(
                new Animal
                {
                    Name = "Dimitri",
                    Age = 1,
                    Id = 0
                }
            );
            context.SaveChanges();
        }
    }
}
