using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

#pragma warning disable CS8618

public class Organisation
{
    public int Id { get; set; }
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string Name { get; set; }
}
