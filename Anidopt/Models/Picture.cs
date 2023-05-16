using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class Picture : EntityModelBase
{
    [Required]
    public byte[] Image { get; set; }

    [Required]
    public int AnimalId { get; set; }

    [Required]
    public bool Showcase { get; set; }

    [ForeignKey(nameof(AnimalId))]
    public virtual Animal? Animal { get; set; }
}
