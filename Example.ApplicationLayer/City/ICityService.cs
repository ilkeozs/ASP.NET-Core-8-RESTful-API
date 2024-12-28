using Example.DomainLayer;

namespace Example.ApplicationLayer;

public interface ICityService
{
    Task Add(City city);
    //List<City> GetCityList();
}