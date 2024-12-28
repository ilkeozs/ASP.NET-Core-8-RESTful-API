using Example.DomainLayer;
using System.Linq.Expressions;

namespace Example.Database.Repositories.Personnels;

public class PersonnelRepository : BaseRepository<Personnel>, IPersonnelRepository
{
    public PersonnelRepository(Context context) : base(context)
    {
    }

    public async Task<Personnel> AddAsync(Personnel personnel)
    {
        await AddAsync(personnel);
        return personnel;
    }

    public async Task<Personnel> Update(Personnel personnel)
    {
        await UpdateAsync(personnel);
        return personnel;
    }

    // 6 references var onları değiştirmedim
    public IQueryable<Personnel> GetPersonnelQueryable(Expression<Func<Personnel, bool>>? expression = null)
    {
        return expression == null ? Queryable() : Queryable(expression);
    }
}