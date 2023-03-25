using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Anidopt.Models;

namespace Anidopt.Data
{
    public class AnidoptContext : DbContext
    {
        public AnidoptContext (DbContextOptions<AnidoptContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AnimalType>()
                .HasIndex(at => at.Name)
                .IsUnique();
            builder.Entity<Organisation>()
                .HasIndex(o => o.Name)
                .IsUnique();
            builder.Entity<Breed>()
                .HasIndex(b => b.Name)
                .IsUnique();
        }

        public DbSet<Anidopt.Models.Animal> Animal { get; set; } = default!;

        public DbSet<Anidopt.Models.AnimalType> AnimalType { get; set; } = default!;

        public DbSet<Anidopt.Models.Organisation> Organisation { get; set; } = default!;

        public DbSet<Anidopt.Models.Breed> Breed { get; set; } = default!;
    }
}
