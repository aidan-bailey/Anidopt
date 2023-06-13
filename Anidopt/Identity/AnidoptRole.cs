using Microsoft.AspNetCore.Identity;

namespace Anidopt.Identity;

public class AnidoptRole : IdentityRole<int> {
    public AnidoptRole() : base() {
    }

    public AnidoptRole(string roleName) : base(roleName) {
    }
}
