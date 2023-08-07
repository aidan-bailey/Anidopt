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

    public Task<AnidoptUser?> GetUserAsync(ClaimsPrincipal principal)
        => _userManager.GetUserAsync(principal);
}
