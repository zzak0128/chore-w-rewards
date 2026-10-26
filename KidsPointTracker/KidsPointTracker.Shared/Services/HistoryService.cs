using KidsPointTracker.Shared.Database;
using KidsPointTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace KidsPointTracker.Shared.Services;

public class HistoryService
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public HistoryService(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<History>> GetHistoryAsync()
    {
        var context = await _contextFactory.CreateDbContextAsync();
        return await context.Histories.OrderBy(x => x.User).ThenBy(x => x.Description).ToListAsync();
    }

    public async Task SaveHistoryAsync(User user, TaskItem taskItem)
    {
        History newHistory = new()
        {
            User = user,
            Description = taskItem.Name,
            PointValue =  taskItem.PointValue,
        };
        
        var context = await _contextFactory.CreateDbContextAsync();
        context.Histories.Add(newHistory);
        await context.SaveChangesAsync();
    }
}