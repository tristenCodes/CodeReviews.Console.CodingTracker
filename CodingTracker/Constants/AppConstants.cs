namespace CodingTracker.Constants;

public static class AppConstants
{
   public const string SelectAllSessionsSql =
      "SELECT id, start_time as StartTime, end_time as EndTime, duration FROM coding_session";
}