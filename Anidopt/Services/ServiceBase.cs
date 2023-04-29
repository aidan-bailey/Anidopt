using Anidopt.Data;
using Anidopt.Models;
using Anidopt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anidopt.Services;

public class ServiceBase<T> : IServiceBase<T> where T : class
{
    private readonly AnidoptContext _context;
    private readonly DbSet<T> _dbSet;

    public bool Initialised => _dbSet != null;

    public ServiceBase(AnidoptContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task EnsureDeletionById(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public bool ExistsById(int id) => _dbSet.Find(id) != null;

    public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}
