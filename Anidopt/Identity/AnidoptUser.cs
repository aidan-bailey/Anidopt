using Anidopt.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anidopt.Identity;

public class AnidoptUser : IdentityUser<int>, IEntityModelBase {
    [DisplayName("First Name")]
    [Required]
    public string FirstName { get; set; } = null!;

    [DisplayName("Last Name")]
    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public int OrganisationId { get; set; }

    [ForeignKey(nameof(OrganisationId))]
    public virtual Organisation? Organisation { get; set; }

    public bool IsOrganisationAdmin { get; set; }

}
