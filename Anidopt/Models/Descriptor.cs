using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class Descriptor
{
    public int Id { get; set; }
    public virtual DescriptorType? DescriptorType { get; set; }
    [Required]
    public int DescriptorTypeId { get; set; }
    [Required]
    public string? Name { get; set; }
}
