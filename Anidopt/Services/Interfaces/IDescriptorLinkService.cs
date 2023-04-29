using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IDescriptorLinkService
{
    bool Initialised { get; }
    Task<List<DescriptorLink>> GetDescriptorLinksAsync();
    Task<DescriptorLink?> GetDescriptorLinkByIdAsync(int id);
    Task<bool> DescriptorLinkExistsByIdAsync(int id);
    Task EnsureDescriptorLinkDeletionByIdAsync(int id);
}
