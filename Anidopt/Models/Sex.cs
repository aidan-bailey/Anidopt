using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class Sex: EntityModelBase, IEntityModelBase {
    #region Native Properties
    [Required]
    public string? Name { get; set; }
    #endregion

    #region Proxy Properties
    public virtual List<Animal> Animals { get; set; }
    #endregion
}
