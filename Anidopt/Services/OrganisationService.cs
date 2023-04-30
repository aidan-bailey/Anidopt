using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class OrganisationService: IOrganisationService
{
    private readonly AnidoptContext _context;

    public OrganisationService(AnidoptContext context)
    {
        _context = context;
    }

    public bool Initialised => _context.Organisation != null;

    public bool ExistsById(int id) => _context.Organisation.Any(e => e.Id == id);

    public async Task EnsureDeletionById(int id)
    {
        var organisation = await GetByIdAsync(id);
        if (organisation != null) _context.Organisation.Remove(organisation);
        await _context.SaveChangesAsync();
    }

    public async Task<Organisation?> GetByIdAsync(int id) => await _context.Organisation.FindAsync(id);

    public async Task<List<Organisation>> GetAllAsync() => await _context.Organisation.ToListAsync();
}
