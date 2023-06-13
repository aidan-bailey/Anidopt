using Anidopt.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class UserOrganisationLink : EntityModelBase, IEntityModelBase
{
    #region Native Properties

    [Required]
    public bool IsAdmin { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int OrganisationId { get; set; }

    #endregion

    #region Proxy Properties

    [ForeignKey(nameof(UserId))]
    public virtual AnidoptUser? User { get; set; }

    [ForeignKey(nameof(OrganisationId))]
    public virtual Organisation? Organisation { get; set; }

    #endregion
}
