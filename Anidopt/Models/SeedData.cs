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
                    },
                    new Species
                    {
                        Name = "Frog"
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
                context.SaveChanges();
            }

            // Look for any movies.
            if (!context.Animal.Any())
            {
                context.Animal.AddRange(
                    new Animal
                    {
                        Name = "Ginny",
                        BirthDay = new DateTime(2016, 12, 16),
                        Organisation = context.Organisation.Where(o => o.Name == "Mdzananda").First(),
                        Breed = context.Breed.Where(b => b.Name == "Afrikanis").First(),
                        Sex = context.Sex.Where(s => s.Name == "Female").First(),
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

            if (!context.DescriptorLink.Any())
            {
                context.DescriptorLink.AddRange(
                    new DescriptorLink
                    {
                        Descriptor = context.Descriptor.Where(at => at.Name == "Dewormed").First(),
                        Animal = context.Animal.Where(at => at.Name == "Ginny").First()
                    },
                    new DescriptorLink
                    {
                        Descriptor = context.Descriptor.Where(at => at.Name == "Dewormed").First(),
                        Animal = context.Animal.Where(at => at.Name == "Layla").First()
                    }
                );
                context.SaveChanges();
            }

            if (!context.Detail.Any())
            {
                context.Detail.AddRange(
                    new Detail
                    {
                        DescriptorLink = context.DescriptorLink.Where(at => at.Descriptor.Name == "Dewormed").First(),
                        Description = "Happened on arrival at Mdzananda Animal Clinic"
                    }
                );
                context.SaveChanges();
            }

        }
    }
}
