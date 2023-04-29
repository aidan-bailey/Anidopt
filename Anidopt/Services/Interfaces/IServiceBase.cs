using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IServiceBase<T> where T : class
{
    bool Initialised { get; }
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    bool ExistsById(int id);
    Task EnsureDeletionById(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
}
