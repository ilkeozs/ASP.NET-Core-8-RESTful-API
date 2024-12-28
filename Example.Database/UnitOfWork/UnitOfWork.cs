using Example.Database.Repositories;
using Example.Database.Repositories.Personnels;

namespace Example.Database.UnitofWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;

    public ICityRepository Cities { get; private set; }
    public IPersonnelRepository Personnels { get; private set; }
    public IDistrictRepository Districts { get; private set; }

    public UnitOfWork(Context context)
    {
        _context = context;
        Cities = new CityRepository(_context);
        Personnels = new PersonnelRepository(_context);
        Districts = new DistrictRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}