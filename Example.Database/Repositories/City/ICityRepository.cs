using Example.DomainLayer;

namespace Example.Database.Repositories;

public interface ICityRepository : IBaseRepository<City>
{
    Task<City> GetCityByName(string cityName);
}