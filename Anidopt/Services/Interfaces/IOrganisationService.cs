using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IOrganisationService
{
    public Task<List<Organisation>> GetOrganisations();
}
