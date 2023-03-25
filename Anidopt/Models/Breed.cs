using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class Breed
{
    public int Id { get; set; }
    [StringLength(32, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }
    public virtual AnimalType? AnimalType { get; set; }
    public int AnimalTypeId { get; set; }
}
