using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface ISexService
{
    bool Initialised { get; }
    Task<List<Sex>> GetSexAsync();
    Task<Sex?> GetSexByIdAsync(int id);
    Task<bool> SexExistsByIdAsync(int id);
    Task EnsureSexDeletionByIdAsync(int id);
}
