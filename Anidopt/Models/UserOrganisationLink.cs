using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class UserOrganisationLink : EntityModelBase
{
    #region Native Properties

    [Required]
    public string UserId { get; set; }

    [Required]
    public int OrganisationId { get; set; }

    [Required]
    public bool IsAdmin { get; set; }

    #endregion

    #region Proxy Properties

    [ForeignKey(nameof(UserId))]
    public virtual AnidoptUser? User { get; set; }

    [ForeignKey(nameof(OrganisationId))]
    public virtual Organisation? Organisation { get; set; }

    #endregion
}
