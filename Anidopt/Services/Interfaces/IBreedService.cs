using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IBreedService
{
    bool Initialised { get; }
    Task<List<Breed>> GetBreedsAsync();
    Task<Breed?> GetBreedByIdAsync(int id);
    Task<List<Breed>> GetBreedsForSpeciesById(int id);
    bool BreedExistsById(int id);
    Task EnsureBreedDeletionById(int id);
    Task<List<Breed>> GetBreedsForSpeciesByIdAsync(int id);
}
