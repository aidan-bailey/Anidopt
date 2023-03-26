using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class DescriptorType
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
}
