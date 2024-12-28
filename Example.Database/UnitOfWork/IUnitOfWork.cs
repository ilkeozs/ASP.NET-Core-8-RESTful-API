using Example.Database.Repositories;
using Example.Database.Repositories.Personnels;

namespace Example.Database.UnitofWork;

public interface IUnitOfWork : IDisposable
{
    ICityRepository Cities { get; }
    IDistrictRepository Districts { get; }
    IPersonnelRepository Personnels { get; }
    Task<int> CompleteAsync();
}