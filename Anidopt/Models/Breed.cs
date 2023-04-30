using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class Breed : EntityModelBase
{
    [StringLength(32, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }
    [ForeignKey(nameof(SpeciesId))]
    public virtual Species? Species { get; set; }
    public int SpeciesId { get; set; }
}
