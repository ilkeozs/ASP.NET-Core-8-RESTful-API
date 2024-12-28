using Example.ApplicationLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers;

[ApiController]
[Route("[controller]")]
public class DistrictController : Controller
{
    private readonly IDistrictService _districtService;

    public DistrictController(IDistrictService districtService)
    {
        _districtService = districtService;
    }

    [HttpGet]
    [Route(nameof(GetDistrictsWithPersonnel))]
    public async Task<IActionResult> GetDistrictsWithPersonnel()
    {
        return Ok(await _districtService.GetAllDistrictsWithPersonnel());
    }

    [HttpGet]
    [Route(nameof(GetDistrictGroup))]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetDistrictGroup()
    {
        return Ok(await _districtService.GetDistrictGroups());
    }
}