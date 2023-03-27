using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface ISpeciesService
{
    public bool Initialised { get; }
    public Task<List<Species>> GetSpeciesAsync();
    public Task<Species?> GetSpeciesByIdAsync(int id);
    public bool GetSpeciesExists(int id);
    public Task EnsureSpeciesDeletionById(int id);
}
