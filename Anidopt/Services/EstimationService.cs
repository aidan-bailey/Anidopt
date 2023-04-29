using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class EstimationService : IEstimationService
{
    private readonly AnidoptContext _context;

    public EstimationService(AnidoptContext context)
    {
        _context = context;
    }

    public bool Initialised => _context.Estimation != null;

    public async Task<List<Estimation>> GetEstimationsAsync() => await _context.Estimation.ToListAsync();

    public async Task<Estimation?> GetEstimationByIdAsync(int id) => await _context.Estimation.FindAsync(id);

    public async Task<bool> EstimationExistsByIdAsync(int id) => await _context.Estimation.AnyAsync(e => e.Id == id);

    public async Task EnsureEstimationDeletionByIdAsync(int id)
    {
        var estimation = await GetEstimationByIdAsync(id);
        if (estimation != null)
            _context.Estimation.Remove(estimation);
        await _context.SaveChangesAsync();
    }
}
