namespace KidsPointTracker.Shared.Models;

public class TaskItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int PointValue { get; set; }
}