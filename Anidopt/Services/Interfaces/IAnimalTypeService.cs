using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IAnimalTypeService
{
    public Task<List<AnimalType>> GetAnimalTypesAsync();
}
