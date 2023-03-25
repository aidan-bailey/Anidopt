using Anidopt.Models;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services.Interfaces;

public interface IAnimalService
{
    public Task<List<Animal>> GetAnimalsAsync();
    public Task<Animal?> GetAnimalByIdAsync(int id);
    public Task<bool> AnimalExistsByIdAsync(int id);
    public Task ConfirmAnimalDeletionByIdAsync(int id);
}
