using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class Descriptor : EntityModelBase
{
    public virtual DescriptorType? DescriptorType { get; set; }
    [Required]
    public int DescriptorTypeId { get; set; }
    [Required]
    public string? Name { get; set; }
}
