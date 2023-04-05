using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IDescriptorService
{
    bool Initialised { get; }
    Task<List<Descriptor>> GetDescriptorsAsync();
    Task<Descriptor?> GetDescriptorByIdAsync(int id);
    Task<bool> DescriptorExistsByIdAsync(int id);
    Task EnsureDescriptorDeletionByIdAsync(int id);
    Task<List<Descriptor>> GetDescriptorsForAnimalByIdAsync(int id);
}
