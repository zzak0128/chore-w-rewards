namespace KidsPointTracker.Shared.Models;
public class Reward
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double PointValue { get; set; }
}