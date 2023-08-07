using System.Security.Claims;
using Anidopt.Identity;

namespace Anidopt.Services.Interfaces;

public interface IAnidoptUserService : IEntityServiceBase<AnidoptUser> {
    Task<AnidoptUser?> GetUserAsync(ClaimsPrincipal principal);
}
