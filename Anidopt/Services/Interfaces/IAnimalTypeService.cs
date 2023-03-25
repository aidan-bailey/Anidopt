using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IAnimalTypeService
{
    public Task<List<AnimalType>> GetAnimalTypesAsync();
    public Task<AnimalType?> GetAnimalTypeByIdAsync(int id);

    public bool GetAnimalTypeExists(int id);
}
