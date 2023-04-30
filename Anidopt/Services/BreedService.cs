using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class BreedService : EntityServiceBase<Breed>, IBreedService
{
    public BreedService(AnidoptContext context) : base(context)
    {
    }

    public async Task<List<Breed>> GetForSpeciesById(int id) => await _dbSet.Where(b => b.SpeciesId == id).ToListAsync();

    public async Task<List<Breed>> GetForSpeciesByIdAsync(int id) => await _dbSet.Where(b => b.SpeciesId == id).ToListAsync();
}
