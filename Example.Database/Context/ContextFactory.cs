using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Example.Database;

public sealed class ContextFactory : IDesignTimeDbContextFactory<Context>
{
public Context CreateDbContext(string[] args)
    {
        const string connectionString = "Server=DESKTOP-N6SQSCT;Database=ExampleDB;Integrated Security=true;TrustServerCertificate=true;";
        return new Context(new DbContextOptionsBuilder<Context>().UseSqlServer(connectionString).Options);
    }
}