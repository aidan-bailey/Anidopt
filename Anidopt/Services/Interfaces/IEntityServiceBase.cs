using Anidopt.Models;

namespace Anidopt.Services.Interfaces;

public interface IEntityServiceBase<T> where T : class, IEntityModelBase {
    bool Initialised { get; }
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(int id);
    bool ExistsById(int id);
    Task<bool> ExistsByIdAsync(int id);
    Task EnsureDeletionByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
}
