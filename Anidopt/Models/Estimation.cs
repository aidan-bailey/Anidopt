using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class Estimation : EntityModelBase
{
    #region Native Properties
    
    [Required]
    [Range(1, float.MaxValue)]
    public float Height { get; set; }

    [Required]
    [Range(1, float.MaxValue)]
    public float Weight { get; set; }

    [Required]
    public int BreedId { get; set; }

    [Required]
    public int SexId { get; set; }

    #endregion

    #region Proxy Properties

    [ForeignKey(nameof(BreedId))]
    public virtual Breed? Breed { get; set; }

    [ForeignKey(nameof(SexId))]
    public virtual Sex? Sex { get; set; }

    #endregion
}
