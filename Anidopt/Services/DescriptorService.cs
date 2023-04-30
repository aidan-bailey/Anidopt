using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class DescriptorService : EntityServiceBase<Descriptor>, IDescriptorService
{
    public DescriptorService(AnidoptContext context) : base(context)
    {
    }

    public async Task<List<Descriptor>> GetForAnimalByIdAsync(int id) => await _context.DescriptorLink.Where(dl => dl.AnimalId == id).Select(dl => dl.Descriptor).ToListAsync();
}
