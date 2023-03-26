using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class InfoLink
{
    public int Id { get; set; }
    public virtual Animal? Animal { get; set; }
    [Required]
    public int AnimalId { get; set; }
    public virtual Descriptor? Descriptor { get; set; }
    [Required]
    public int DescriptorId { get; set; }
    public virtual Detail? Detail { get; set; }
    [Required]
    public int DetailId { get; set; }
}
