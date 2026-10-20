using KidsPointTracker.Shared.Database;
using KidsPointTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace KidsPointTracker.Shared.Services;

public class TaskService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    
    public TaskService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<TaskItem>> GetTasksAsync()
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.TaskItems.OrderBy((x => x.Name)).ToListAsync();
    }
    
    public async Task CreateTaskItemAsync(TaskItem taskItem)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        context.TaskItems.Add(taskItem);
        await context.SaveChangesAsync();
    }

    public async Task UpdateTaskItemAsync(TaskItem item)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        
        TaskItem? updateTask = await context.TaskItems.FindAsync(item.Id);
        
        if (updateTask is null)
        {
            throw new Exception("TaskItem not found");
        }
        
        updateTask.Name = item.Name;
        updateTask.PointValue = item.PointValue;

        context.TaskItems.Update(updateTask);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTaskItemAsync(TaskItem item)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        context.TaskItems.Remove(item);
        await context.SaveChangesAsync();
    }
}