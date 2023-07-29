using System.Security.Claims;
using Anidopt.Identity;
using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IAnidoptUserService : IEntityServiceBase<AnidoptUser> {
    Task<List<Organisation>> GetAdministratedOrganisationsAsync(int id);
    Task<List<AnidoptUser>> GetAdministratedUsersAsync(int id);
    Task<AnidoptUser?> GetUserAsync(ClaimsPrincipal principal);
    Task<bool> HasEditRights(ClaimsPrincipal principal, int id);
}
