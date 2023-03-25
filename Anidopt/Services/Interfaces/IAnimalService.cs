using Anidopt.Models;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services.Interfaces;

public interface IAnimalService
{
    public Task<List<AnimalType>> GetAnimalTypes();
    public Task<List<Animal>> GetAnimals();
    public Task<Animal?> GetAnimalById(int id);
    public Task<bool> AnimalExistsById(int id);
    public Task ConfirmAnimalDeletionById(int id);
}
