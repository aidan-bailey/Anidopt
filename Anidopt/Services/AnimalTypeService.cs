using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class AnimalTypeService: IAnimalTypeService
{
    private readonly AnidoptContext _context;

    public AnimalTypeService(AnidoptContext context)
    {
        _context = context;
    }

    public bool Initialised => _context.Breed != null;

    public async Task<AnimalType?> GetAnimalTypeByIdAsync(int id) => await _context.AnimalType.FindAsync(id);

    public async Task<List<AnimalType>> GetAnimalTypesAsync() => await _context.AnimalType.ToListAsync();

    public bool GetAnimalTypeExists(int id) => _context.AnimalType.Any(e => e.Id == id);

    public async Task ConfirmDeletionById(int id)
    {
        var animalType = await GetAnimalTypeByIdAsync(id);
        if (animalType != null) _context.AnimalType.Remove(animalType);
        await _context.SaveChangesAsync();
    }
}
