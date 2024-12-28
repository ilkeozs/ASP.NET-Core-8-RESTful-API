using Example.ApplicationLayer.Personnels;
using Example.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonnelController : Controller
{
    private readonly IPersonnelService _personnelService;

    public PersonnelController(IPersonnelService personnelService)
    {
        _personnelService = personnelService;
    }

    [HttpPut]
    [Route(nameof(AddAsync))]
    public async Task<IActionResult> AddAsync(AddPersonnelModel model)
    {
        var result = await _personnelService.AddAsync(model);
        return Ok(result);
    }

    [HttpPut]
    [Route(nameof(UpdateAsync))]
    public async Task<IActionResult> UpdateAsync(AddPersonnelModel model)
    {
        var result = await _personnelService.UpdateAsync(model);
        return Ok(result);
    }

    [HttpDelete]
    [Route(nameof(DeleteAsync))]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _personnelService.DeleteAsync(id);
        return Ok(result);
    }

    [HttpPut]
    [Route(nameof(AddPersonnelInfoAsync))]
    public async Task<IActionResult> AddPersonnelInfoAsync(AddPersonnelInfoModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _personnelService.AddPersonnelAsync(model);
        return Ok(result);
    }

    [HttpPost]
    [Route(nameof(GetPersonnelList))]
    public async Task<IActionResult> GetPersonnelList(PersonnelFilterModel model)
    {
        var result = await _personnelService.GetPersonnelList(model);
        return Ok(result);
    }

    [HttpGet]
    [Route(nameof(GetPersonnelWithDistrictList))]
    public async Task<IActionResult> GetPersonnelWithDistrictList()
    {
        var result = await _personnelService.GetPersonnelDistrictJoin();
        return Ok(result);
    }

    [HttpGet]
    [Route(nameof(GetPersonnelDistinct))]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetPersonnelDistinct()
    {
        var result = await _personnelService.GetPersonnelDistinct();
        return Ok(result);
    }

    [HttpGet]
    [Route(nameof(GetPersonnel))]
    public async Task<IActionResult> GetPersonnel(string name)
    {
        var result = await _personnelService.GetPersonnel(name);
        return Ok(result);
    }
}