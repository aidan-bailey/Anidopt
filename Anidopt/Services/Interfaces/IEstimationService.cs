using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IEstimationService
{
    bool Initialised { get; }
    Task<List<Estimation>> GetEstimationsAsync();
    Task<Estimation?> GetEstimationByIdAsync(int id);
    Task<bool> EstimationExistsByIdAsync(int id);
    Task EnsureEstimationDeletionByIdAsync(int id);
}
