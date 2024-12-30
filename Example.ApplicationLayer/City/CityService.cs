using Example.Database.UnitofWork;
using Example.DomainLayer;
using Example.DomainLayer.Shared;
using Example.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Example.ApplicationLayer;

public class CityService : ICityService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _memoryCache;

    public CityService(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
    {
        _unitOfWork = unitOfWork;
        _memoryCache = memoryCache;
    }

    public async Task Add(City city)
    {
        await _unitOfWork.Cities.AddAsync(city);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<List<CityCache>> CityList()
    {
        if (!_memoryCache.TryGetValue(CacheKeys.CityList, out List<CityCache> cityCachedData))
        {
            var cityList = await _unitOfWork.Cities.Queryable().Select(s => new CityCache
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

            _memoryCache.Set<List<CityCache>>(CacheKeys.CityList, cityList, cacheEntryOptions);
        }
        return _memoryCache.Get<List<CityCache>>(CacheKeys.CityList);
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