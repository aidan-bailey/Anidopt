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

            context.Database.EnsureDeleted(); // TODO - this is obviously bad!!!
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

            if (!context.Sex.Any())
            {
                context.Sex.AddRange(
                    new Sex
                    {
                        Name = "Male"
                    },
                    new Sex
                    {
                        Name = "Female"
                    }
                );
                context.SaveChanges();
            }

            if (!context.DescriptorType.Any())
            {
                context.DescriptorType.AddRange(
                    new DescriptorType
                    {
                        Name = "Social"
                    },
                    new DescriptorType
                    {
                        Name = "Medical"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Descriptor.Any())
            {
                context.Descriptor.AddRange(
                    new Descriptor
                    {
                        Name = "Friendly with Cats",
                        DescriptorType = context.DescriptorType.Where(at => at.Name == "Social").First()
                    },
                    new Descriptor
                    {
                        Name = "Dewormed",
                        DescriptorType = context.DescriptorType.Where(at => at.Name == "Medical").First()
                    }
                );
                context.SaveChanges();
            }

            if (!context.Species.Any())
            {
                context.Species.AddRange(
                    new Species
                    {
                        Name = "Dog"
                    },
                    new Species
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
                        Species = context.Species.Where(at => at.Name == "Dog").First()
                    },
                    new Breed
                    {
                        Name = "Siamese",
                        Species = context.Species.Where(at => at.Name == "Cat").First()
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
                        Species = context.Species.Where(at => at.Name == "Dog").First(),
                        Organisation = context.Organisation.Where(o => o.Name == "Mdzananda").First(),
                        Breed = context.Breed.Where(b => b.Name == "Golden Retriever").First()
                    }
                ); ;
                context.SaveChanges();
            }

        }
    }
}
