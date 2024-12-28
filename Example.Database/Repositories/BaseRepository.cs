using Example.DomainLayer;
using Example.DomainLayer.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace Example.Database.Repositories;

public class BaseRepository<T> where T : class
{
    private readonly Context _context;

    private DbSet<T> _dbSet => _context.Set<T>();

    public BaseRepository(Context context)
    {
        _context = context;
    }

    public async Task AddAsync(T record)
    {
        await _dbSet.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T record)
    {
        _dbSet.Update(record);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T record)
    {
        PropertyInfo status = record.GetType().GetProperty(nameof(BaseClass.Status));
        if (status != null)
        {
            status.SetValue(record, DataStatus.Deleted);
            await UpdateAsync(record);

        }
    }

    public async Task<T> Get(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public IQueryable<T> Queryable(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where("Status==" + (int)DataStatus.Active).Where(expression);
    }

    // Overload
    public IQueryable<T> Queryable()
    {
        return _dbSet.Where("Status==" + (int)DataStatus.Active);
    }
}