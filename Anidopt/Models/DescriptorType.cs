using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class DescriptorType : EntityModelBase
{
    [Required]
    public string? Name { get; set; }

    public virtual List<Descriptor> Descriptors { get; set; } = new List<Descriptor>();
}
