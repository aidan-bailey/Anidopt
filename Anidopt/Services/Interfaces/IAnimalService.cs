using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IAnimalService
{
    public Task<List<AnimalType>> GetAnimalTypes();
}
