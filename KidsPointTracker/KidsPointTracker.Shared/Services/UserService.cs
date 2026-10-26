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

    public async Task CreateUserAsync(string name, UserType userType)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var newUser = new User
        {
            Name = name,
            UserType = userType
        };
        context.Users.Add(newUser);
        await context.SaveChangesAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var updateUser = await context.Users.FindAsync(user.Id);
        if (updateUser is null)
        {
            throw new Exception("User not found");
        }

        updateUser.Name = user.Name;
        updateUser.UserType = user.UserType;
        context.Users.Update(updateUser);

        await context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User item)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        context.Users.Remove(item);
        await context.SaveChangesAsync();
    }
}