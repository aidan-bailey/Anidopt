using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class SizeEstimation
{
    public int Id { get; set; }

    [Required]
    public float Height { get; set; }

    [Required]
    public float Weight { get; set; }

    [Required]
    public int BreedId { get; set; }

    [Required]
    public int SexId { get; set; }
}
