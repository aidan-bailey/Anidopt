using System.ComponentModel.DataAnnotations;
using Anidopt.Identity;

namespace Anidopt.Models;

#pragma warning disable CS8618

public class Organisation : EntityModelBase, IEntityModelBase {
    #region Native Properties

    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string Name { get; set; }

    #endregion

    #region Proxy Properties

    public virtual List<Animal>? Animals { get; set; }

    public virtual List<AnidoptUser> Users { get; set; }

    #endregion
}
