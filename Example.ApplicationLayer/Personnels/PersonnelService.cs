using Example.Database.UnitofWork;
using Example.DomainLayer;
using Example.DomainLayer.Shared;
using Example.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Example.ApplicationLayer.Personnels;

public class PersonnelService : IPersonnelService
{
    private readonly IUnitOfWork _unitOfWork;

    public PersonnelService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddPersonnelModel> AddAsync(AddPersonnelModel model)
    {
        try
        {
            var personnel = new Personnel
            {
                BirthDate = model.BirthDate,
                DistrictId = model.DistrictId,
                FullName = model.FullName,
                Gender = model.Gender
            };
            await _unitOfWork.Personnels.AddAsync(personnel);
            await _unitOfWork.CompleteAsync();
            model.Id = personnel.Id;
            return model;
        }
        catch (Exception exp)
        {
            throw new Exception(exp?.Message);
        }
    }

    public async Task<AddPersonnelModel> UpdateAsync(AddPersonnelModel model)
    {
        try
        {
            var record = await _unitOfWork.Personnels.Get(model.Id);
            if (record != null)
            {
                record.FullName = model.FullName;
                record.Gender = model.Gender;
                record.BirthDate = model.BirthDate;
                record.DistrictId = model.DistrictId;

                _unitOfWork.Personnels.Update(record);
                await _unitOfWork.CompleteAsync();
                return model;
            }
            return new AddPersonnelModel();
        }
        catch (Exception exp)
        {
            throw new Exception(exp?.Message);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var record = await _unitOfWork.Personnels.Get(id);
            if (record != null)
            {
                record.Status = DataStatus.Deleted;

                _unitOfWork.Personnels.Update(record);

                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
        catch (Exception exp)
        {
            throw new Exception(exp?.Message);
        }
    }

    public async Task<AddPersonnelInfoModel> AddPersonnelAsync(AddPersonnelInfoModel model)
    {
        try
        {
            var personnel = new Personnel
            {
                Gender = model.Gender,
                BirthDate = model.BirthDate,
                FullName = model.FullName
            };

            var district = await _unitOfWork.Districts.GetDistrictByCityAndName(model.CityName, model.DistrictName);
            if (district != null)
            {
                personnel.DistrictId = district.Id;

                await _unitOfWork.Personnels.AddAsync(personnel);
                await _unitOfWork.CompleteAsync();

                model.Id = personnel.Id;
                return model;
            }

            var city = await _unitOfWork.Cities.GetCityByName(model.CityName);
            if (city != null)
            {
                var newDistrictForCity = new District
                {
                    CityId = city.Id,
                    Name = model.DistrictName
                };

                await _unitOfWork.Districts.AddAsync(newDistrictForCity);
                await _unitOfWork.CompleteAsync();

                personnel.DistrictId = newDistrictForCity.Id;
                await _unitOfWork.Personnels.AddAsync(personnel);
                await _unitOfWork.CompleteAsync();

                model.Id = personnel.Id;
                return model;
            }

            var newCity = new City
            {
                Name = model.CityName
            };

            await _unitOfWork.Cities.AddAsync(newCity);
            await _unitOfWork.CompleteAsync();

            var newDistrict = new District
            {
                Name = model.DistrictName,
                CityId = newCity.Id
            };

            await _unitOfWork.Districts.AddAsync(newDistrict);
            await _unitOfWork.CompleteAsync();

            personnel.DistrictId = newDistrict.Id;
            await _unitOfWork.Personnels.AddAsync(personnel);
            await _unitOfWork.CompleteAsync();

            model.Id = personnel.Id;
            return model;
        }
        catch (Exception exp)
        {
            throw new Exception(exp?.Message);
        }
    }

    public async Task<dynamic> GetPersonnelList(PersonnelFilterModel filter)
    {
        var query = _unitOfWork.Personnels.GetPersonnelQueryable();

        if (filter.Gender.HasValue)
            query = query.Where(s => s.Gender == filter.Gender.Value);

        if (!string.IsNullOrEmpty(filter.SearchName))
            query = query.Where(s => s.FullName.Contains(filter.SearchName));

        if (filter.BirthYear.HasValue)
            query = query.Where(s => s.BirthDate.Year == filter.BirthYear.Value);

        if (!string.IsNullOrEmpty(filter.CityName))
            query = query.Where(s => s.District.City.Name.Contains(filter.CityName));

        if (filter.DistrictNames.Any())
            query = query.Where(s => filter.DistrictNames.Any(c => c.Contains(s.District.Name)));

        var querySelect = query
        .Select(s => new
        {
            s.FullName,
            s.BirthDate,
            s.Gender,
            DistrictName = s.District.Name
        });

        querySelect = querySelect.Skip(filter.Index).Take(filter.PageCount);

        var list = await querySelect.ToListAsync();
        return list;
    }

    public async Task<dynamic> GetPersonnelDistrictJoin()
    {
        var personnelQuery = _unitOfWork.Personnels.GetPersonnelQueryable();
        var districtQuery = _unitOfWork.Districts.Queryable();

        // Join query
        var join = await personnelQuery.Join(districtQuery,
            p => p.DistrictId,
            d => d.Id,
            (p, d) => new
            {
                p.BirthDate,
                p.FullName,
                DistrictName = d.Name
            }).ToListAsync();
        return join;
    }

    public async Task<dynamic> GetPersonnelDistinct()
    {
        var query = await _unitOfWork.Personnels.GetPersonnelQueryable().Select(s => s.FullName).Distinct().ToListAsync();
        return query;
    }

    public async Task<dynamic> GetPersonnel(string name)
    {
        var query = _unitOfWork.Personnels.GetPersonnelQueryable();

        var first = await query.Where(s => s.FullName == name).FirstAsync(); // There must be one or more, there cannot be none 

        var firstOrDefault = await query.Where(s => s.FullName == name).FirstOrDefaultAsync(); // It doesn't matter if there is one or more or none

        var single = await query.Where(s => s.FullName == name).SingleAsync(); // There can only be one, there cannot be more than one or none

        var singleOrDefault = await query.Where(s => s.FullName == name).SingleOrDefaultAsync(); // There can be only one or none, there cannot be more than one

        return new { first, firstOrDefault, single, singleOrDefault };
    }
}