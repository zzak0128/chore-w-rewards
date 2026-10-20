using KidsPointTracker.Shared.Database;
using KidsPointTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace KidsPointTracker.Shared.Services;

public class RewardService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    
    public RewardService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Reward>> GetRewardsAsync()
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Rewards.OrderBy(x => x.Name).ToListAsync();
    }
    
    public async Task CreateRewardAsync(Reward item)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        context.Rewards.Add(item);
        await context.SaveChangesAsync();
    }

    public async Task UpdateRewardAsync(Reward item)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        
        Reward? updateReward = await context.Rewards.FindAsync(item.Id);
        
        if (updateReward is null)
        {
            throw new Exception("Reward not found");
        }
        
        updateReward.Name = item.Name;
        updateReward.PointValue = item.PointValue;

        context.Rewards.Update(updateReward);
        await context.SaveChangesAsync();
    }

    public async Task DeleteRewardAsync(Reward item)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        context.Rewards.Remove(item);
        await context.SaveChangesAsync();
    }
}