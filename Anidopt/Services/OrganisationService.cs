using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Services;

public class OrganisationService : EntityServiceBase<Organisation>, IOrganisationService
{
    public OrganisationService(AnidoptContext context) : base(context)
    {
    }
}
