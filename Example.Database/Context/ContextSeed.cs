using Example.DomainLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Database;

public static class ContextSeed
{
    public static void Seed(this ModelBuilder builder)
    {
        builder.Entity<City>().HasData(new City
        {
            Id = 1,
            Name = "Istanbul",
        });
    }

    public static async Task CreateIdmAdminUser(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        string adminUserName = "admin@example.com";

        if (await userManager.FindByNameAsync(adminUserName) == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = adminUserName,
                Email = adminUserName
            };

            var result = await userManager.CreateAsync(adminUser, "Admin35*");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}