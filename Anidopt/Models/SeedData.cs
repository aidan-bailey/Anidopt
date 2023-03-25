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

            var changed = false;

            if (!context.AnimalType.Any())
            {
                context.AnimalType.AddRange(
                    new AnimalType
                    {
                        Name = "Dog",
                        Id = 0
                    },
                    new AnimalType
                    {
                        Name = "Cat",
                        Id = 0
                    }
                );
                changed = true;
            }

            // Look for any movies.
            if (!context.Animal.Any())
            {
                context.Animal.AddRange(
                    new Animal
                    {
                        Name = "Dimitri",
                        Age = 1,
                        Id = 0
                    }
                );
                changed = true;
            }

            if (changed) context.SaveChanges();
        }
    }
}
