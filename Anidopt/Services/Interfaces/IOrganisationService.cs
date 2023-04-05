using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IOrganisationService
{
    bool Initialised { get; }
    Task<List<Organisation>> GetOrganisationsAsync();
    Task<Organisation?> GetOrganisationByIdAsync(int id);
    bool OrganisationExistsById(int id);
    Task EnsureOrganisationDeletionById(int id);
}
