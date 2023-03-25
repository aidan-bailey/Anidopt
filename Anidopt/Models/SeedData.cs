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

            if (!context.AnimalType.Any())
            {
                context.AnimalType.AddRange(
                    new AnimalType
                    {
                        Name = "Dog"
                    },
                    new AnimalType
                    {
                        Name = "Cat"
                    }
                );
                context.SaveChanges();
            }

            // Look for any movies.
            if (!context.Animal.Any())
            {
                context.Animal.AddRange(
                    new Animal
                    {
                        Name = "Dimitri",
                        Age = 1,
                        AnimalType = context.AnimalType.Where(at => at.Name == "Dog").First()
                    }
                ); ;
                context.SaveChanges();
            }

        }
    }
}
