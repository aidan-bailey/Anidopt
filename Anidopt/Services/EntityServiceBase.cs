using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public partial class EntityServiceBase<T> : IEntityServiceBase<T> where T : class, IEntityModelBase {

    internal readonly AnidoptContext _context;
    internal readonly DbSet<T> _dbSet;

    public bool Initialised => _dbSet != null;

    public EntityServiceBase(AnidoptContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task EnsureDeletionByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public bool ExistsById(int id) => _dbSet.Any(e => e.Id == id);

    public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByIdAsync(int id) => (await _dbSet.FindAsync(id)) != null;
}
