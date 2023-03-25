using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class AnimalService : IAnimalService
{
    private readonly AnidoptContext _context;

    public AnimalService(AnidoptContext context)
    {
        _context = context;
    }

    public async Task<List<AnimalType>> GetAnimalTypesAsync() => await _context.AnimalType.ToListAsync();

    public async Task<List<Animal>> GetAnimalsAsync() => await _context.Animal.ToListAsync();

    public async Task<Animal?> GetAnimalByIdAsync(int id) => await _context.Animal.FindAsync(id);

    public async Task<bool> AnimalExistsByIdAsync(int id) => await _context.Animal.AnyAsync(e => e.Id == id);

    public async Task ConfirmAnimalDeletionByIdAsync(int id)
    {
        var animal = await GetAnimalByIdAsync(id);
        if (animal != null)
            _context.Animal.Remove(animal);
        await _context.SaveChangesAsync();
    }
}
