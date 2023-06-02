using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class AnidoptUser : IdentityUser
{
    [DisplayName("First Name")]
    [Required]
    public string FirstName { get; set; } = null!;

    [DisplayName("Last Name")]
    [Required]
    public string LastName { get; set; } = null!;

    [DisplayName("Organisations")]
    public virtual List<UserOrganisationLink>? UserOrganisationLinks { get; set; }
}
