using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IOrganisationService
{
    public bool Initialised { get; }
    public Task<List<Organisation>> GetOrganisationsAsync();
    public Task<Organisation?> GetOrganisationByIdAsync(int id);
    public bool OrganisationExistsById(int id);
    public Task EnsureOrganisationDeletionById(int id);
}
