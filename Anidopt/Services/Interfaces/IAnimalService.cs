using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IAnimalService
{
    bool Initialised { get; }
    Task<List<Animal>> GetAnimalsAsync();
    Task<Animal?> GetAnimalByIdAsync(int id);
    Task<bool> AnimalExistsByIdAsync(int id);
    Task EnsureAnimalDeletionByIdAsync(int id);
}
