using LibraryApp.Domain.Entities;
using LibraryApp.Infrastructure.Data;

namespace LibraryApp.Infrastructure.Seeders;

public class DatabaseSeeder(ApplicationDbContext dbContext)
{
    public async Task SeedRoles()
    {
        if (dbContext.UserEntityRoles.Any()) return;

        var roles = new List<UserEntityRole>
        {
            new() { Name = "User" },
            new() { Name = "Admin" }
        };

        await dbContext.UserEntityRoles.AddRangeAsync(roles);
        await dbContext.SaveChangesAsync();
    }
}