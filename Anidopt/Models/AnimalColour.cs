using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class AnimalColour : EntityModelBase
{
    #region Native Properties

    [Required]
    public string Colour { get; set; } = default!;

    #endregion

    #region Proxy Properties

    public virtual List<AnimalColourLink> AnimalColourLinks { get; set; } = new List<AnimalColourLink>();

    #endregion
}
