namespace Example.DomainLayer;

public class City : BaseClass
{
    public string Name { get; set; }
    public virtual ICollection<District> Districts { get; set; } //Eager Loading
}