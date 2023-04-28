using Anidopt.Data;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Models;

public static class SeedData
{

    private static void Seed<T>(this AnidoptContext context, params T[] entities) where T : class
    {
        var dbSet = context.Set<T>();
        if (dbSet.Any()) return;
        dbSet.AddRange(entities);
        context.SaveChanges();
    }

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AnidoptContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<AnidoptContext>>()))
        {

            context.Database.EnsureDeleted(); // TODO - this is obviously bad!!!
            context.Database.EnsureCreated();

            // ORGANISATION

            context.Seed(
                new Organisation
                {
                    Name = "Mdzananda"
                }
            );

            // SEX

            context.Seed(
                new Sex
                {
                    Name = "Male"
                },
                new Sex
                {
                    Name = "Female"
                }
            );

            // SPECIES

            context.Seed(
                new Species
                {
                    Name = "Dog"
                },
                new Species
                {
                    Name = "Cat"
                },
                new Species
                {
                    Name = "Frog"
                }
            );

            // BREED

            context.Seed(
                new Breed
                {
                    Name = "Golden Retriever",
                    Species = context.Species.Where(at => at.Name == "Dog").First()
                },
                new Breed
                {
                    Name = "Afrikanis",
                    Species = context.Species.Where(at => at.Name == "Dog").First()
                },
                new Breed
                {
                    Name = "Siamese",
                    Species = context.Species.Where(at => at.Name == "Cat").First()
                },
                new Breed
                {
                    Name = "Red-Eyed Tree",
                    Species = context.Species.Where(at => at.Name == "Frog").First()
                }
            );

            // ANIMAL

            context.Seed(
                new Animal
                {
                    Name = "Ginny",
                    BirthDay = new DateTime(2016, 12, 16),
                    Organisation = context.Organisation.Where(o => o.Name == "Mdzananda").First(),
                    Breed = context.Breed.Where(b => b.Name == "Afrikanis").First(),
                    Sex = context.Sex.Where(s => s.Name == "Female").First(),
                    Description = "Ginny is a playful little pup who loves a good snooze.",
                    Weight = 0,
                    Height = 0
                },
                new Animal
                {
                    Name = "Layla",
                    BirthDay = new DateTime(2016, 12, 16),
                    Organisation = context.Organisation.Where(o => o.Name == "Mdzananda").First(),
                    Breed = context.Breed.Where(b => b.Name == "Afrikanis").First(),
                    Sex = context.Sex.Where(s => s.Name == "Female").First(),
                    Weight = 0,
                    Height = 0
                }
            );

            // DESCRIPTOR TYPE

            context.Seed(
                new DescriptorType
                {
                    Name = "Social"
                },
                new DescriptorType
                {
                    Name = "Medical"
                },
                new DescriptorType
                {
                    Name = "Personal"
                }
            );

            // DESCRIPTOR

            context.Seed(
                new Descriptor
                {
                    Name = "Friendly with Cats",
                    DescriptorType = context.DescriptorType.Where(at => at.Name == "Social").First()
                },
                new Descriptor
                {
                    Name = "Nervous",
                    DescriptorType = context.DescriptorType.Where(at => at.Name == "Personal").First()
                },
                new Descriptor
                {
                    Name = "Dewormed",
                    DescriptorType = context.DescriptorType.Where(at => at.Name == "Medical").First()
                },
                new Descriptor
                {
                    Name = "Vaccinated",
                    DescriptorType = context.DescriptorType.Where(at => at.Name == "Medical").First()
                }
            );

            // DESCRIPTOR LINK

            context.Seed(
                new DescriptorLink
                {
                    Descriptor = context.Descriptor.Where(at => at.Name == "Dewormed").First(),
                    Animal = context.Animal.Where(at => at.Name == "Ginny").First()
                },
                new DescriptorLink
                {
                    Descriptor = context.Descriptor.Where(at => at.Name == "Nervous").First(),
                    Animal = context.Animal.Where(at => at.Name == "Ginny").First()
                },
                new DescriptorLink
                {
                    Descriptor = context.Descriptor.Where(at => at.Name == "Dewormed").First(),
                    Animal = context.Animal.Where(at => at.Name == "Layla").First()
                }
            );

        }
    }
}
