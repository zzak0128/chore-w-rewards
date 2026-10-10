namespace KidsPointTracker.Shared;

public class History
{
    public int Id { get; set; }

    public User User { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int PointValue { get; set; }
}