using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface ISpeciesService
{
    bool Initialised { get; }
    Task<List<Species>> GetSpeciesAsync();
    Task<Species?> GetSpeciesByIdAsync(int id);
    bool SpeciesExistsById(int id);
    Task EnsureSpeciesDeletionById(int id);
}
