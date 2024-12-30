using Example.DomainLayer;
using Example.ViewModel;

namespace Example.ApplicationLayer;

public interface ICityService
{
    Task Add(City city);
    Task<List<CityCache>> CityList();
    //List<City> GetCityList();
}