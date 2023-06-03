using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class AnimalColourLink : EntityModelBase
{
    #region Native Properties

    [Required]
    public int? AnimalId { get; set; }

    [Required]
    public int? ColourId { get; set; }

    #endregion

    #region Proxy Properties

    [ForeignKey(nameof(AnimalId))]
    public virtual Animal? Animal { get; set; }

    [ForeignKey(nameof(ColourId))]
    public virtual AnimalColour? Colour { get; set; }

    #endregion
}
