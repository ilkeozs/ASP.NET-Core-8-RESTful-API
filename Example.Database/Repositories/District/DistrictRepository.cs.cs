using Example.DomainLayer;
using Microsoft.EntityFrameworkCore;

namespace Example.Database.Repositories;

public class DistrictRepository : BaseRepository<District>, IDistrictRepository
{
    public DistrictRepository(Context context) : base(context)
    {
    }

    public async Task<District> GetDistrictByCityAndName(string cityName, string districtName)
    {
        var record = await Queryable(s => s.City.Name == cityName && s.Name == districtName).FirstOrDefaultAsync();
        return record;
    }
}