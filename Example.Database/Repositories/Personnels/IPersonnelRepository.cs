using Example.DomainLayer;
using System.Linq.Expressions;

namespace Example.Database.Repositories.Personnels;

public interface IPersonnelRepository : IBaseRepository<Personnel>
{
    Task<Personnel> AddAsync(Personnel personnel);
    Task<Personnel> Update(Personnel personnel);
    IQueryable<Personnel> GetPersonnelQueryable(Expression<Func<Personnel, bool>>? expression = null);
}