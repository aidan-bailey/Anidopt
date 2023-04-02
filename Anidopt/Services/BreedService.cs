using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class BreedService : IBreedService
{
    private readonly AnidoptContext _context;

    public BreedService(AnidoptContext context)
    {
        _context = context;
    }

    public bool Initialised => _context.Breed != null;

    public bool BreedExistsById(int id) => _context.Breed.Any(e => e.Id == id);

    public async Task EnsureBreedDeletionById(int id)
    {
        var breed = await GetBreedByIdAsync(id);
        if (breed != null) _context.Breed.Remove(breed);
        await _context.SaveChangesAsync();
    }

    public async Task<Breed?> GetBreedByIdAsync(int id) => await _context.Breed.FindAsync(id);

    public async Task<List<Breed>> GetBreedsAsync() => await _context.Breed.ToListAsync();

    public async Task<List<Breed>> GetBreedsForSpeciesById(int id) => await _context.Breed.Where(b => b.SpeciesId == id).ToListAsync();

    public async Task<List<Breed>> GetBreedsForSpeciesByIdAsync(int id) => await _context.Breed.Where(b => b.SpeciesId == id).ToListAsync();
}
