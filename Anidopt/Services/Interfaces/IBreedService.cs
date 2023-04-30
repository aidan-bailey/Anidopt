using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IBreedService : IEntityServiceBase<Breed>
{
    Task<List<Breed>> GetForSpeciesById(int id);
    Task<List<Breed>> GetForSpeciesByIdAsync(int id);
}
