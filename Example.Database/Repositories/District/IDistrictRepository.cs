using Example.DomainLayer;

namespace Example.Database.Repositories;

public interface IDistrictRepository : IBaseRepository<District>
{
    Task<District> GetDistrictByCityAndName(string cityName, string districtName);
}