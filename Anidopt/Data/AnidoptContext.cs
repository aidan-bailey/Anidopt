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
            // UserOrganisationLink One-To-Many
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
            // AnidoptUser Many-To-One
            builder.Entity<UserOrganisationLink>()
                .HasOne(uol => uol.User)
                .WithMany(au => au.UserOrganisationLinks)
                .HasForeignKey(au => au.UserId)
                .HasPrincipalKey(uol => uol.Id);
            // Organisation Many-To-One
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
            // UserOrganisationLink One-To-Many
            builder.Entity<Organisation>()
                .HasMany(o => o.UserOrganisationLinks)
                .WithOne(uol => uol.Organisation)
                .HasForeignKey(uol => uol.OrganisationId)
                .HasPrincipalKey(o => o.Id);
            // Animal One-To-Many
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
            // Descriptor One-To-Many
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
            // DescriptorType One-To-Many
            builder.Entity<Descriptor>()
                .HasOne(d => d.DescriptorType)
                .WithMany(dt => dt.Descriptors)
                .HasForeignKey(dt => dt.DescriptorTypeId)
                .HasPrincipalKey(d => d.Id);
            // DescriptorLink One-To-Many
            builder.Entity<Descriptor>()
                .HasMany(d => d.DescriptorLinks)
                .WithOne(dl => dl.Descriptor)
                .HasForeignKey(dl => dl.DescriptorId)
                .HasPrincipalKey(d => d.Id);
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
