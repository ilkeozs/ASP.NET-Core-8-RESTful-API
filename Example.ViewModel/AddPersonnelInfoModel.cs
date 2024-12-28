using Example.DomainLayer.Shared;
using System.ComponentModel.DataAnnotations;

namespace Example.ViewModel;

public class AddPersonnelInfoModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter full name.")]
    [StringLength(100, ErrorMessage = "Full name must be less than 100 characters.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Please enter birth date.")]
    [DataType(DataType.Date, ErrorMessage = "Birth date must be a valid date.")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "Please enter gender.")]
    public GenderType Gender { get; set; }

    [Required(ErrorMessage = "Please enter city name.")]
    [StringLength(100, ErrorMessage = "City name must be less than 100 characters.")]
    public string CityName { get; set; }

    [StringLength(100, ErrorMessage = "District name must be less than 100 characters.")]
    public string DistrictName { get; set; }
}