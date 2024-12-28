using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Example.Database;

public sealed class Context : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public Context(DbContextOptions<Context> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        modelBuilder.Seed();
    }
}