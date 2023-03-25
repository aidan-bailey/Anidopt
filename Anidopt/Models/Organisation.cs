using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

#pragma warning disable CS8618

public class Organisation
{
    public int Id;
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string Name;
    public List<Animal> Animals;
    public List<int> AnimalIds;
}
