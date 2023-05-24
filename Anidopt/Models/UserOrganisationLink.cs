using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Models;

public class UserOrganisationLink : EntityModelBase
{
    [Required]
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual AnidoptUser? User { get; set; }

    [Required]
    public int OrganisationId { get; set; }

    [ForeignKey(nameof(OrganisationId))]
    public virtual Organisation? Organisation { get; set; }

    [Required]
    public bool IsAdmin { get; set; }
}
