namespace Example.DomainLayer;

public class District : BaseClass
{
    public int CityId { get; set; }
    public City City { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Personnel> Personnels { get; set; } //Eager Loading
}