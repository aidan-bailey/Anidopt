using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

[PrimaryKey(nameof(Id))]
public class Species
{
    public int Id { get; set; }

    [StringLength(32, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }
}
