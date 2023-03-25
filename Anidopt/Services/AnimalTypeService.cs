using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class AnimalTypeService : IAnimalTypeService
{
    private readonly AnidoptContext _context;

    public AnimalTypeService(AnidoptContext context)
    {
        _context = context;
    }

    public async Task<List<AnimalType>> GetAnimalTypes() => await _context.AnimalType.ToListAsync();
}
