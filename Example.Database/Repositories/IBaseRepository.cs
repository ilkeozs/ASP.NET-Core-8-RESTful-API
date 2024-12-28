using System.Linq.Expressions;

namespace Example.Database.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task AddAsync(T record);
    Task UpdateAsync(T record);
    Task DeleteAsync(T record);
    Task<T> Get(object id);
    IQueryable<T> Queryable(Expression<Func<T, bool>> expression);
    IQueryable<T> Queryable();
}