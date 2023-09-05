using Anidopt.Identity;
using Anidopt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Anidopt.Data;

public static class SeedData
{

    private static AnidoptContext Seed<T>(this AnidoptContext context, params T[] entities) where T : class
    {
        var dbSet = context.Set<T>();
        if (dbSet.Any()) return context;
        dbSet.AddRange(entities);
        context.SaveChanges();
        return context;
    }

    private static AnidoptContext SeedSexes(this AnidoptContext context) => context.Seed(
        new Sex
        {
            Name = "Male"
        },
        new Sex
        {
            Name = "Female"
        }
    );

    private static AnidoptContext SeedSpecies(this AnidoptContext context) => context.Seed(
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

    private static AnidoptContext SeedBreeds(this AnidoptContext context) => context.Seed(
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

    private static AnidoptContext SeedAnimals(this AnidoptContext context) => context.Seed(
        new Animal
        {
            Name = "Ginny",
            BirthDay = new DateTime(2016, 12, 16),
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
            Breed = context.Breed.Where(b => b.Name == "Afrikanis").First(),
            Sex = context.Sex.Where(s => s.Name == "Female").First(),
            Weight = 0,
            Height = 0
        }
    );

    private static AnidoptContext SeedDescriptorTypes(this AnidoptContext context) => context.Seed(
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

    private static AnidoptContext SeedDescriptors(this AnidoptContext context) => context.Seed(
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

    private static AnidoptContext SeedDescriptorLinks(this AnidoptContext context) => context.Seed(
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

    private static AnidoptContext SeedRoles(this AnidoptContext context, IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<AnidoptRole>>();
        roleManager.CreateAsync(new AnidoptRole("SiteAdmin")).Wait();
        roleManager.CreateAsync(new AnidoptRole("OrganisationAdmin")).Wait();
        roleManager.CreateAsync(new AnidoptRole("OrganisationUser")).Wait();
        return context;
    }

    private static AnidoptContext SeedUsers(this AnidoptContext context, IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<AnidoptUser>>();
        var siteAdmin = new AnidoptUser
        {
            FirstName = "Site",
            LastName = "Admin",
            UserName = "admin@anidopt.org",
            Email = "admin@anidopt.org",
            IsOrganisationAdmin = true
        };
        userManager.CreateAsync(siteAdmin, "1").Wait();
        userManager.AddToRoleAsync(siteAdmin, "SiteAdmin").Wait();

        var organisationAdmin = new AnidoptUser
        {
            FirstName = "Organisation",
            LastName = "Admin",
            UserName = "admin@testorganisation.org",
            Email = "admin@testorganisation.org",
            IsOrganisationAdmin = true
        };
        userManager.CreateAsync(organisationAdmin, "1").Wait();
        userManager.AddToRoleAsync(organisationAdmin, "OrganisationAdmin").Wait();

        var organisationUser = new AnidoptUser
        {
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

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AnidoptContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<AnidoptContext>>()))
        {

            context.Database.EnsureDeleted(); // TODO - this is obviously bad!!!
            context.Database.EnsureCreated();
            context
                .SeedSexes()
                .SeedSpecies()
                .SeedBreeds()
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
