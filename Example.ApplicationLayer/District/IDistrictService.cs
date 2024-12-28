namespace Example.ApplicationLayer;

public interface IDistrictService
{
    Task<dynamic> GetAllDistrictsWithPersonnel();
    Task<dynamic> GetDistrictGroups();
}