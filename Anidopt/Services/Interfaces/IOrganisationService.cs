using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IOrganisationService
{
    bool Initialised { get; }
    Task<List<Organisation>> GetAllAsync();
    Task<Organisation?> GetByIdAsync(int id);
    bool ExistsById(int id);
    Task EnsureDeletionById(int id);
}
