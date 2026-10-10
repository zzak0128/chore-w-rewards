namespace KidsPointTracker.Shared;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public UserType UserType { get; set; }
}