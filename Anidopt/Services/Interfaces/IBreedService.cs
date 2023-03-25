using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IBreedService
{
    public bool Initialised { get; }
    public Task<List<Breed>> GetBreedsAsync();
    public Task<Breed?> GetBreedByIdAsync(int id);
    public bool BreedExistsById(int id);
}
