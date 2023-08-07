using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class OrganisationService : EntityServiceBase<Organisation>, IOrganisationService
{
    public OrganisationService(AnidoptContext context) : base(context)
    {
        
    }

    public IQueryable<Organisation> GetAdministratedByUserId(int id) {
        return GetAll()
            .Include(o => o.UserOrganisationLinks)
            .SelectMany(o => o.UserOrganisationLinks)
            .Include(uol => uol.Organisation)
            .Where(uol => uol.IsAdmin)
            .Select(uol => uol.Organisation!);
    }
}
