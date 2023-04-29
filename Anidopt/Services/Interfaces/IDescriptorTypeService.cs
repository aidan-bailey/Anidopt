using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IDescriptorTypeService
{
    bool Initialised { get; }
    Task<List<DescriptorType>> GetDescriptorTypesAsync();
    Task<DescriptorType?> GetDescriptorTypeByIdAsync(int id);
    Task<bool> DescriptorTypeExistsByIdAsync(int id);
    Task EnsureDescriptorTypeDeletionByIdAsync(int id);
}
