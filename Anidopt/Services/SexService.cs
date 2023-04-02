using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class SexService: ISexService
{
    private readonly AnidoptContext _context;

    public SexService(AnidoptContext context)
    {
        _context = context;
    }

    public bool Initialised => _context.Sex != null;

    public async Task<List<Sex>> GetSexAsync() => await _context.Sex.ToListAsync();

    public async Task<Sex?> GetSexByIdAsync(int id) => await _context.Sex.FindAsync(id);

    public async Task<bool> SexExistsByIdAsync(int id) => await _context.Sex.AnyAsync(e => e.Id == id);

    public async Task EnsureSexDeletionByIdAsync(int id)
    {
        var sex = await GetSexByIdAsync(id);
        if (sex != null)
            _context.Sex.Remove(sex);
        await _context.SaveChangesAsync();
    }
}
