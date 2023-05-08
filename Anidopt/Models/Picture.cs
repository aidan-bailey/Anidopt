using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class Picture : EntityModelBase
{
    [Required]
    public byte[] Image { get; set; } = new byte[] { };

    [Required]
    public int AnimalId { get; set; }

    [Required]
    public bool Showcase { get; set; }

    [ForeignKey(nameof(AnimalId))]
    public Animal Animal { get; set; }
}
