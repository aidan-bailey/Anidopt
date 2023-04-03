using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class Detail
{
    public int Id { get; set; }
    [Required]
    public string? Description { get; set; }
    public virtual DescriptorLink? DescriptorLink { get; set; }
    [Required]
    public int DescriptorLinkId { get; set; }
}
