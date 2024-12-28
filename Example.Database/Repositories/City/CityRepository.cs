using Example.DomainLayer;
using Microsoft.EntityFrameworkCore;

namespace Example.Database.Repositories;

public class CityRepository : BaseRepository<City>, ICityRepository
{
    public CityRepository(Context context) : base(context)
    {

    }

    public async Task<City> GetCityByName(string cityName)
    {
        var record = await base.Queryable(s => s.Name == cityName).FirstOrDefaultAsync();
        return record;
    }
}