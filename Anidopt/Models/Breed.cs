using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class Breed
{
    public int Id { get; set; }
    [StringLength(32, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }
    [ForeignKey(nameof(AnimalTypeId))]
    public virtual AnimalType? AnimalType { get; set; }
    public int AnimalTypeId { get; set; }
    public virtual List<Animal> Animals { get; set; } = new List<Animal>();
}
