using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IAnimalService
{
    public Task<List<AnimalType>> GetAnimalTypes();
    public Task<Animal?> GetAnimalById(int id);
    public Task<bool> AnimalExistsById(int id);
    public Task ConfirmAnimalDeletionById(int id);
}
