using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class AnimalType
{
    public int Id { get; set; }
    [StringLength(32, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }
}
