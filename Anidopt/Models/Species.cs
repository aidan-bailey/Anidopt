﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class Species: EntityModelBase, IEntityModelBase {
    [StringLength(32, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }

    public virtual List<Breed> Breeds { get; set; } = new List<Breed>();
}
