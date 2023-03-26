using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class Animal
{
    public int Id { get; set; }
    [StringLength(32, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int Age { get; set; }
    public string Description { get; set; } = "";

    [ForeignKey(nameof(AnimalTypeId))]
    public virtual AnimalType? AnimalType { get; set; }
    [Required]
    public int AnimalTypeId { get; set; }

    public virtual Organisation? Organisation { get; set; }
    [Required]
    public int OrganisationId { get; set; }

    [ForeignKey(nameof(BreedId))]
    public virtual Breed? Breed { get; set; }
    [Required]
    public int BreedId { get; set; }
}
