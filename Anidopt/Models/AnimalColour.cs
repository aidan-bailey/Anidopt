using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class AnimalColour : EntityModelBase
{
    [Required]
    public string Colour { get; set; } = default!;
}
