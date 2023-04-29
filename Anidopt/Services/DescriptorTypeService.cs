using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class DescriptorTypeService : IDescriptorTypeService
{
    private readonly AnidoptContext _context;

    public DescriptorTypeService(AnidoptContext context)
    {
        _context = context;
    }

    public bool Initialised => _context.DescriptorType != null;

    public async Task<List<DescriptorType>> GetDescriptorTypesAsync() => await _context.DescriptorType.ToListAsync();

    public async Task<DescriptorType?> GetDescriptorTypeByIdAsync(int id) => await _context.DescriptorType.FindAsync(id);

    public async Task<bool> DescriptorTypeExistsByIdAsync(int id) => await _context.DescriptorType.AnyAsync(e => e.Id == id);

    public async Task EnsureDescriptorTypeDeletionByIdAsync(int id)
    {
        var descriptorType = await GetDescriptorTypeByIdAsync(id);
        if (descriptorType != null)
            _context.DescriptorType.Remove(descriptorType);
        await _context.SaveChangesAsync();
    }
}
