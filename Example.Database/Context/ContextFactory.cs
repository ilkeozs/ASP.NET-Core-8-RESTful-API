using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Example.Database;

public sealed class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        const string connectionString = "Host=db;Database=TestDB;Username=postgres;Password=089077;Port=5432;";
        return new Context(new DbContextOptionsBuilder<Context>().UseNpgsql(connectionString).Options);
    }
}