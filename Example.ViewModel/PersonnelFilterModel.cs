using Example.DomainLayer.Shared;

namespace Example.ViewModel;

public class PersonnelFilterModel
{
    public string SearchName { get; set; }
    public int? BirthYear { get; set; }
    public GenderType? Gender { get; set; }
    public string CityName { get; set; }
    public List<string> DistrictNames { get; set; }
    public int Index { get; set; }
    public int PageCount { get; set; }
}