using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IAnimalService
{
    public bool Initialised { get; }
    public Task<List<Animal>> GetAnimalsAsync();
    public Task<Animal?> GetAnimalByIdAsync(int id);
    public Task<bool> AnimalExistsByIdAsync(int id);
    public Task EnsureAnimalDeletionByIdAsync(int id);
}
