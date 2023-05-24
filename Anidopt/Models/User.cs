using Microsoft.AspNetCore.Identity;

namespace Anidopt.Models;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<UserOrganisationLink>? UserOrganisationLinks { get; set; }
}
