using Example.Database.UnitofWork;
using Example.DomainLayer;

namespace Example.ApplicationLayer;

public class CityService : ICityService
{

    private readonly IUnitOfWork _unitOfWork;

    public CityService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Add(City city)
    {
        await _unitOfWork.Cities.AddAsync(city);
        await _unitOfWork.CompleteAsync();
    }

    //public readonly List<City> Cities = new List<City>()
    //{
    //    new City(){Name="İzmir", Code = "IZM"},
    //    new City(){Name="İstanbul", Code = "IST"}
    //};

    //public List<City> GetCityList()
    //{
    //    return Cities;
    //}
}