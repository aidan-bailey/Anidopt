using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IDescriptorService : IEntityServiceBase<Descriptor>
{
    Task<List<Descriptor>> GetForAnimalByIdAsync(int id);
}
