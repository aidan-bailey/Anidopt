using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class DescriptorLinkService : IDescriptorLinkService
{
    private readonly AnidoptContext _context;

    public DescriptorLinkService(AnidoptContext context)
    {
        _context = context;
    }

    public bool Initialised => _context.DescriptorLink != null;

    public async Task<List<DescriptorLink>> GetDescriptorLinksAsync() => await _context.DescriptorLink.ToListAsync();

    public async Task<DescriptorLink?> GetDescriptorLinkByIdAsync(int id) => await _context.DescriptorLink.FindAsync(id);

    public async Task<bool> DescriptorLinkExistsByIdAsync(int id) => await _context.DescriptorLink.AnyAsync(e => e.Id == id);

    public async Task EnsureDescriptorLinkDeletionByIdAsync(int id)
    {
        var descriptorLink = await GetDescriptorLinkByIdAsync(id);
        if (descriptorLink != null)
            _context.DescriptorLink.Remove(descriptorLink);
        await _context.SaveChangesAsync();
    }
}
