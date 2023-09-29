using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class Breed : EntityModelBase, IEntityModelBase {
    #region Native Properties
    
    [StringLength(64, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }

    public int SpeciesId { get; set; }

    #endregion

    #region Proxy Properties

    [ForeignKey(nameof(SpeciesId))]
    public virtual Species? Species { get; set; }

    public virtual List<Animal> Animals { get; set; }

    #endregion
}
