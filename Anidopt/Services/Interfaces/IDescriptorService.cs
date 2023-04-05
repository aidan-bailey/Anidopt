using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IDescriptorService
{
    public bool Initialised { get; }
    public Task<List<Descriptor>> GetDescriptorsAsync();
    public Task<Descriptor?> GetDescriptorByIdAsync(int id);
    public Task<bool> DescriptorExistsByIdAsync(int id);
    public Task EnsureDescriptorDeletionByIdAsync(int id);
}
