using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class SpeciesService: ISpeciesService
{
    private readonly AnidoptContext _context;

    public SpeciesService(AnidoptContext context)
    {
        _context = context;
    }

    public bool Initialised => _context.Breed != null;

    public async Task<Species?> GetSpeciesByIdAsync(int id) => await _context.Species.FindAsync(id);

    public async Task<List<Species>> GetSpeciesAsync() => await _context.Species.ToListAsync();

    public bool GetSpeciesExists(int id) => _context.Species.Any(e => e.Id == id);

    public async Task EnsureSpeciesDeletionById(int id)
    {
        var Species = await GetSpeciesByIdAsync(id);
        if (Species != null) _context.Species.Remove(Species);
        await _context.SaveChangesAsync();
    }
}
