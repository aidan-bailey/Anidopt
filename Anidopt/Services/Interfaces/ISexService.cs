using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface ISexService
{
    public bool Initialised { get; }
    public Task<List<Sex>> GetSexAsync();
    public Task<Sex?> GetSexByIdAsync(int id);
    public Task<bool> SexExistsByIdAsync(int id);
    public Task EnsureSexDeletionByIdAsync(int id);
}
