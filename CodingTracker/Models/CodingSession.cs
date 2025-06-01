namespace CodingTracker.Models;

public class CodingSession
{
    private DateTime _startTime;

    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }
}
