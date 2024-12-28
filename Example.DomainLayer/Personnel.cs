using Example.DomainLayer.Shared;

namespace Example.DomainLayer;

public class Personnel : BaseClass
{
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderType Gender { get; set; }
    public int DistrictId { get; set; }
    public District District { get; set; }
}