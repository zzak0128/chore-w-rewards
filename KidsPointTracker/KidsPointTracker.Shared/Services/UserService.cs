using KidsPointTracker.Shared.Database;
using KidsPointTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace KidsPointTracker.Shared.Services;

public class UserService
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public UserService(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        var context = await _contextFactory.CreateDbContextAsync();
        return await context.Users.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task SaveUserAsync(string name, UserType userType)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        User newUser = new User
        {
            Name = name,
            UserType = userType
        };
        context.Users.Add(newUser);
        await context.SaveChangesAsync();
    }
}