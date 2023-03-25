using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class Animal
{

    public int Id { get; set; }
    [StringLength(32, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }
    [Required]
    public int Age { get; set; }

}
