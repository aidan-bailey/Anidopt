using System.Security.Claims;
using Anidopt.Data;
using Anidopt.Identity;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class AnidoptUserService : EntityServiceBase<AnidoptUser>, IAnidoptUserService {
    private readonly UserManager<AnidoptUser> _userManager;

    public AnidoptUserService(AnidoptContext context, UserManager<AnidoptUser> userManager) : base(context) {
        _userManager = userManager;
    }

    public Task<List<Organisation>> GetAdministratedOrganisationsAsync(int id) 
        => _context.UserOrganisationLink
                .Where(uol => uol.Id == id)
                .Include(uol => uol.Organisation)
                .Select(uol => uol.Organisation!)
                .ToListAsync();

    private IQueryable<AnidoptUser> GetAdministratedUsersQuery(int id)
        => _context.UserOrganisationLink
                .Where(uol => uol.UserId == id && uol.IsAdmin)
                .Include(uol => uol.Organisation)
                .Include(uol => uol.Organisation!.UserOrganisationLinks)
                .SelectMany(uol => uol.Organisation!.UserOrganisationLinks)
                .Include(uol => uol.User)
                .Select(uol => uol.User!)
                .DistinctBy(au => au.UserName);

    public Task<List<AnidoptUser>> GetAdministratedUsersAsync(int id)
        => GetAdministratedUsersQuery(id).ToListAsync();

    public Task<AnidoptUser?> GetUserAsync(ClaimsPrincipal principal)
        => _userManager.GetUserAsync(principal);

    public async Task<bool> HasEditRights(ClaimsPrincipal principal, int id) {
        var user = await GetUserAsync(principal);
        if (user == null) {
            return false;
        }
        if (user.Id == id) {
            return true;
        }
        var administratedUsersQuery = GetAdministratedUsersQuery(user.Id);
        return administratedUsersQuery.Any(u => u.Id == id);
    }
}
