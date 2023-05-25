using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class AnimalColourLink : EntityModelBase
{
    [Required]
    public string? AnimalId { get; set; }

    [ForeignKey(nameof(AnimalId))]
    public virtual Animal? Animal { get; set; }

    [Required]
    public int? ColourId { get; set; }

    [ForeignKey(nameof(ColourId))]
    public virtual AnimalColour? Colour { get; set; }
}
