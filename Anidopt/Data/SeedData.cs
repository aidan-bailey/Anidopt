using Anidopt.Identity;
using Anidopt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Anidopt.Data;

public static class SeedData {

    private static AnidoptContext Seed<T>(this AnidoptContext context, params T[] entities) where T : class {
        var dbSet = context.Set<T>();
        if (dbSet.Any())
            return context;
        dbSet.AddRange(entities);
        context.SaveChanges();
        return context;
    }

    private static AnidoptContext SeedSexes(this AnidoptContext context) => context.Seed(
        new Sex {
            Name = "Male"
        },
        new Sex {
            Name = "Female"
        }
    );

    private static AnidoptContext SeedSpecies(this AnidoptContext context) => context.Seed(
        new Species {
            Name = "Dog"
        },
        new Species {
            Name = "Cat"
        }
    );

    private static AnidoptContext SeedBreeds(this AnidoptContext context) => context.Seed(
        new Breed {
            Name = "Afghan Hound",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Afrikanis",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Airedale Terrier",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Alsation / German Shepherd",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Basset Hound",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Bloodhound",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Boerboel",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Boxer",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Bull Terrier",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Bulldog",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Cairn Terrier",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Chihuahua",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Collie",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Corgi",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Cross-breed",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Dachshund",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Dalmatian",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Doberman Pinscher",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "English Setter",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Fox Terrier",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Golden Retriever",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Gordon Setter",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Great Dane",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Greyhound",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Irish Terrier",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Irish Wolfhound",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Irish Setter",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Jack Russell Terrier",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "King Charles Spaniel",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Labrador",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Malamute",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Mastiff",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Miniature Toy Pom - Pomeranian",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Miniature Pinscher (Minpin)",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Norfolk Terrier",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Old English Sheepdog",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Pekingese",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Pitbull",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Pointer",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Pointer - Wire Haired",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Pomeranian",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Poodle",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Pug",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Red/Red&White Setter",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Retriever (Flat Coated)",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Ridgeback",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Rottweiler",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Saint Bernard",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Schipperkes",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Schnauzer",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Scottish Terrier",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Setter",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Shar-pei",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Siberian Husky",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Spaniel",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Staffordshire Bull Terrier (Staffie)",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Toy Pom",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Weimaraner",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Whippet",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Yorkshire Terrier (Yorkie)",
            Species = context.Species.Where(at => at.Name == "Dog").First()
        },
        new Breed {
            Name = "Siamese",
            Species = context.Species.Where(at => at.Name == "Cat").First()
        }
    );

    private static AnidoptContext SeedSizes(this AnidoptContext context) => context.Seed(
        new Size {
            Name = "Small"
        },
        new Size {
            Name = "Small-Medium"
        },
        new Size {
            Name = "Medium"
        },
        new Size {
            Name = "Medium-Large"
        },
        new Size {
            Name = "Large"
        }

    );

    private static AnidoptContext SeedAnimals(this AnidoptContext context) => context.Seed(
        new Animal {
            Name = "Ginny",
            BirthDay = new DateTime(2016, 12, 16),
            Breed = context.Breed.Where(b => b.Name == "Afrikanis").First(),
            Sex = context.Sex.Where(s => s.Name == "Female").First(),
            Description = "Ginny is a playful little pup who loves a good snooze.",
            Size = context.Size.Where(s => s.Name == "Small").First(),
            Weight = 50
        },
        new Animal {
            Name = "Layla",
            BirthDay = new DateTime(2016, 12, 16),
            Breed = context.Breed.Where(b => b.Name == "Afrikanis").First(),
            Sex = context.Sex.Where(s => s.Name == "Female").First(),
            Size = context.Size.Where(s => s.Name == "Small-Medium").First(),
            Weight = 50
        }
    );

    private static AnidoptContext SeedDescriptorTypes(this AnidoptContext context) => context.Seed(
        new DescriptorType {
            Name = "Social"
        },
        new DescriptorType {
            Name = "Medical"
        },
        new DescriptorType {
            Name = "Personal"
        }
    );

    private static AnidoptContext SeedDescriptors(this AnidoptContext context) => context.Seed(
        new Descriptor {
            Name = "Friendly with Cats",
            DescriptorType = context.DescriptorType.Where(at => at.Name == "Social").First()
        },
        new Descriptor {
            Name = "Nervous",
            DescriptorType = context.DescriptorType.Where(at => at.Name == "Personal").First()
        },
        new Descriptor {
            Name = "Dewormed",
            DescriptorType = context.DescriptorType.Where(at => at.Name == "Medical").First()
        },
        new Descriptor {
            Name = "Vaccinated",
            DescriptorType = context.DescriptorType.Where(at => at.Name == "Medical").First()
        }
    );

    private static AnidoptContext SeedDescriptorLinks(this AnidoptContext context) => context.Seed(
        new DescriptorLink {
            Descriptor = context.Descriptor.Where(at => at.Name == "Dewormed").First(),
            Animal = context.Animal.Where(at => at.Name == "Ginny").First()
        },
        new DescriptorLink {
            Descriptor = context.Descriptor.Where(at => at.Name == "Nervous").First(),
            Animal = context.Animal.Where(at => at.Name == "Ginny").First()
        },
        new DescriptorLink {
            Descriptor = context.Descriptor.Where(at => at.Name == "Dewormed").First(),
            Animal = context.Animal.Where(at => at.Name == "Layla").First()
        }
    );

    private static AnidoptContext SeedRoles(this AnidoptContext context, IServiceProvider serviceProvider) {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<AnidoptRole>>();
        roleManager.CreateAsync(new AnidoptRole("SiteAdmin")).Wait();
        roleManager.CreateAsync(new AnidoptRole("OrganisationAdmin")).Wait();
        roleManager.CreateAsync(new AnidoptRole("OrganisationUser")).Wait();
        return context;
    }

    private static AnidoptContext SeedUsers(this AnidoptContext context, IServiceProvider serviceProvider) {
        var userManager = serviceProvider.GetRequiredService<UserManager<AnidoptUser>>();
        var siteAdmin = new AnidoptUser {
            FirstName = "Site",
            LastName = "Admin",
            UserName = "admin@anidopt.org",
            Email = "admin@anidopt.org",
            IsOrganisationAdmin = true
        };
        userManager.CreateAsync(siteAdmin, "1").Wait();
        userManager.AddToRoleAsync(siteAdmin, "SiteAdmin").Wait();

        var organisationAdmin = new AnidoptUser {
            FirstName = "Organisation",
            LastName = "Admin",
            UserName = "admin@testorganisation.org",
            Email = "admin@testorganisation.org",
            IsOrganisationAdmin = true
        };
        userManager.CreateAsync(organisationAdmin, "1").Wait();
        userManager.AddToRoleAsync(organisationAdmin, "OrganisationAdmin").Wait();

        var organisationUser = new AnidoptUser {
            FirstName = "Jane",
            LastName = "Doe",
            UserName = "jane@testorganisation.org",
            Email = "jane@testorganisation.org",
            IsOrganisationAdmin = false
        };
        userManager.CreateAsync(organisationUser, "1").Wait();
        userManager.AddToRoleAsync(organisationUser, "OrganisationUser").Wait();

        return context;
    }

    public static void Initialize(IServiceProvider serviceProvider) {
        using (var context = new AnidoptContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<AnidoptContext>>())) {

            context.Database.EnsureDeleted(); // TODO - this is obviously bad!!!
            context.Database.EnsureCreated();
            context
                .SeedSexes()
                .SeedSpecies()
                .SeedBreeds()
                .SeedSizes()
                .SeedAnimals()
                .SeedDescriptorTypes()
                .SeedDescriptors()
                .SeedDescriptorLinks()
                .SeedRoles(serviceProvider)
                .SeedUsers(serviceProvider);

            context.SaveChanges();
        }
    }
}
