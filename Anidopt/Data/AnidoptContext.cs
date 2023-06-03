using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Anidopt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Anidopt.Data
{
    public class AnidoptContext : IdentityDbContext
    {
        public AnidoptContext(DbContextOptions<AnidoptContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            #region AnidoptUser Constraints
            // Unique UserName
            builder.Entity<AnidoptUser>()
                .HasIndex(at => at.UserName)
                .IsUnique();
            // One-To-Many UserOrganisationLink
            builder.Entity<AnidoptUser>()
                .HasMany(au => au.UserOrganisationLinks)
                .WithOne(uol => uol.User)
                .HasForeignKey(uol => uol.UserId)
                .HasPrincipalKey(au => au.Id);
            #endregion
            #region UserOrganisationLink Constraints
            // UserId, OrganisationId index
            builder.Entity<UserOrganisationLink>()
                .HasIndex(uol => new { uol.UserId, uol.OrganisationId })
                .IsUnique();
            // Many-to-One AnidoptUser
            builder.Entity<UserOrganisationLink>()
                .HasOne(uol => uol.User)
                .WithMany(au => au.UserOrganisationLinks)
                .HasForeignKey(au => au.UserId)
                .HasPrincipalKey(uol => uol.Id);
            // Many-to-One Organisation
            builder.Entity<UserOrganisationLink>()
                .HasOne(uol => uol.Organisation)
                .WithMany(o => o.UserOrganisationLinks)
                .HasForeignKey(o => o.OrganisationId)
                .HasPrincipalKey(uol => uol.Id);
            #endregion
            #region Organisation Constraints
            // Organisation index
            builder.Entity<Organisation>()
                .HasIndex(o => o.Name)
                .IsUnique();
            // One-To-Many UserOrganisationLink
            builder.Entity<Organisation>()
                .HasMany(o => o.UserOrganisationLinks)
                .WithOne(uol => uol.Organisation)
                .HasForeignKey(uol => uol.OrganisationId)
                .HasPrincipalKey(o => o.Id);
            // One-To-Many Animal
            builder.Entity<Organisation>()
                .HasMany(o => o.Animals)
                .WithOne(a => a.Organisation)
                .HasForeignKey(a => a.OrganisationId)
                .HasPrincipalKey(o => o.Id);
            #endregion
            #region DescriptorType Constraints
            // Name index
            builder.Entity<DescriptorType>()
                .HasIndex(dt => dt.Name)
                .IsUnique();
            // One-To-Many Descriptor
            builder.Entity<DescriptorType>()
                .HasMany(dt => dt.Descriptors)
                .WithOne(d => d.DescriptorType)
                .HasForeignKey(d => d.DescriptorTypeId)
                .HasPrincipalKey(dt => dt.Id);
            #endregion
            #region Descriptor Constraints
            // Name, DescriptorType index
            builder.Entity<Descriptor>()
                .HasIndex(d => new { d.Name, d.DescriptorTypeId })
                .IsUnique();
            // Many-To-One DescriptorType
            builder.Entity<Descriptor>()
                .HasOne(d => d.DescriptorType)
                .WithMany(dt => dt.Descriptors)
                .HasForeignKey(dt => dt.DescriptorTypeId)
                .HasPrincipalKey(d => d.Id);
            // One-To-Many DescriptorLink
            builder.Entity<Descriptor>()
                .HasMany(d => d.DescriptorLinks)
                .WithOne(dl => dl.Descriptor)
                .HasForeignKey(dl => dl.DescriptorId)
                .HasPrincipalKey(d => d.Id);
            #endregion
            #region DescriptorLink Constraints
            // AnimalId, DescriptorId index
            builder.Entity<DescriptorLink>()
                .HasIndex(dl => new { dl.AnimalId, dl.DescriptorId })
                .IsUnique();
            // Many-To-One Descriptor
            builder.Entity<DescriptorLink>()
                .HasOne(dl => dl.Descriptor)
                .WithMany(d => d.DescriptorLinks)
                .HasForeignKey(d => d.DescriptorId)
                .HasPrincipalKey(dl => dl.Id);
            // Many-To-One Animal
            builder.Entity<DescriptorLink>()
                .HasOne(dl => dl.Animal)
                .WithMany(a => a.DescriptorLinks)
                .HasForeignKey(a => a.AnimalId)
                .HasPrincipalKey(dl => dl.Id);
            #endregion
        }

        public DbSet<Anidopt.Models.Animal> Animal { get; set; } = default!;

        public DbSet<Anidopt.Models.Species> Species { get; set; } = default!;

        public DbSet<Anidopt.Models.Organisation> Organisation { get; set; } = default!;

        public DbSet<Anidopt.Models.Breed> Breed { get; set; } = default!;

        public DbSet<Anidopt.Models.DescriptorType> DescriptorType { get; set; } = default!;

        public DbSet<Anidopt.Models.Descriptor> Descriptor { get; set; } = default!;

        public DbSet<Anidopt.Models.DescriptorLink> DescriptorLink { get; set; } = default!;

        public DbSet<Anidopt.Models.Sex> Sex { get; set; } = default!;

        public DbSet<Anidopt.Models.Estimation> Estimation { get; set; } = default!;

        public DbSet<Anidopt.Models.Picture> Picture { get; set; } = default!;

        public DbSet<Anidopt.Models.UserOrganisationLink> UserOrganisationLink { get; set; } = default!;

        public DbSet<Anidopt.Models.AnidoptUser> AnidoptUser { get; set; } = default!;

        public DbSet<Anidopt.Models.AnimalColour> AnimalColour { get; set; } = default!;

        public DbSet<Anidopt.Models.AnimalColourLink> AnimalColourLink { get; set; } = default!;
    }
}
