using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class Species: EntityBase
{
    [StringLength(32, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }
}
