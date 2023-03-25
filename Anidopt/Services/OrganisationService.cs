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

    public async Task<List<Organisation>> GetOrganisations() =>  await _context.Organisation.ToListAsync();
}
