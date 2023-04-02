using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IBreedService
{
    public bool Initialised { get; }
    public Task<List<Breed>> GetBreedsAsync();
    public Task<Breed?> GetBreedByIdAsync(int id);
    public Task<List<Breed>> GetBreedsForSpeciesById(int id);
    public bool BreedExistsById(int id);
    public Task EnsureBreedDeletionById(int id);
    public Task<List<Breed>> GetBreedsForSpeciesByIdAsync(int id);
}
