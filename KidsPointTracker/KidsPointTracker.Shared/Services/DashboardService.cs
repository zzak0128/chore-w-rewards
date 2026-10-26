using KidsPointTracker.Shared.Database;
using KidsPointTracker.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace KidsPointTracker.Shared.Services;

public class DashboardService
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
    private readonly HistoryService _historyService;

    public DashboardService(IDbContextFactory<ApplicationDbContext> contextFactory, HistoryService historyService)
    {
        _contextFactory = contextFactory;
        _historyService = historyService;
    }

    public async Task<List<DashboardItem>> GetDashboardItemsAsync()
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var histories =  await context.Histories.Include(x => x.User).ToListAsync();
        List<DashboardItem> dashboardItems = [];

        var user = histories.Select(x => x.User).DistinctBy(x => x.Name);
        foreach (var item in user)
        {
            dashboardItems.Add(new DashboardItem()
            {
                UserName = item.Name,
                Points = histories.Where(x => x.User.Name == item.Name).Sum(x => x.PointValue)
            });
        }
        
        //var dashboardItems = await histories.GroupBy(x => x.User.Name).ToListAsync();
        return dashboardItems;
    }
}