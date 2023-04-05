using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;

namespace Anidopt.Services;

public class DescriptorService : IDescriptorService
{
    private readonly AnidoptContext _context;

    public DescriptorService(AnidoptContext context)
    {
        _context = context;
    }

    public bool Initialised => _context.Descriptor != null;

    public async Task<List<Descriptor>> GetDescriptorsAsync() => await _context.Descriptor.ToListAsync();

    public async Task<Descriptor?> GetDescriptorByIdAsync(int id) => await _context.Descriptor.FindAsync(id);

    public async Task<bool> DescriptorExistsByIdAsync(int id) => await _context.Descriptor.AnyAsync(e => e.Id == id);

    public async Task EnsureDescriptorDeletionByIdAsync(int id)
    {
        var descriptor = await GetDescriptorByIdAsync(id);
        if (descriptor != null)
            _context.Descriptor.Remove(descriptor);
        await _context.SaveChangesAsync();
    }
}
