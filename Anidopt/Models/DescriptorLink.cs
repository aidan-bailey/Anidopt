using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class DescriptorLink : EntityModelBase
{
    public virtual Animal? Animal { get; set; }
    [Required]
    public int AnimalId { get; set; }
    public virtual Descriptor? Descriptor { get; set; }
    [Required]
    public int DescriptorId { get; set; }
}
