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

            context.Database.EnsureCreated();

            if (!context.Organisation.Any())
            {
                context.Organisation.AddRange(
                    new Organisation
                    {
                        Name = "Mdzananda"
                    }
                );
                context.SaveChanges();
            }

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

            if (!context.Breed.Any())
            {
                context.Breed.AddRange(
                    new Breed
                    {
                        Name = "Golden Retriever",
                        AnimalType = context.AnimalType.Where(at => at.Name == "Dog").First()
                    },
                    new Breed
                    {
                        Name = "Siamese",
                        AnimalType = context.AnimalType.Where(at => at.Name == "Cat").First()
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
                        AnimalType = context.AnimalType.Where(at => at.Name == "Dog").First(),
                        Organisation = context.Organisation.Where(o => o.Name == "Mdzananda").First(),
                    }
                ); ;
                context.SaveChanges();
            }

        }
    }
}
