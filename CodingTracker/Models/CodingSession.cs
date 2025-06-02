namespace CodingTracker.Models;

public class CodingSession
{
    public int Id { get; set; }
    public required string StartTime { get; set; }
    public required string EndTime { get; set; }
    public string Duration { get; set; }
}
