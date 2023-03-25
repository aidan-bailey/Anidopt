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

        public DbSet<Anidopt.Models.Animal> Animal { get; set; } = default!;

        public DbSet<Anidopt.Models.AnimalType> AnimalType { get; set; } = default!;
    }
}
