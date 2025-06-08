namespace CodingTracker.Utility;

public static class DateTimeHelper
{
   public static DateTime ConvertStringToDateTime(string str)
   {
      var result = DateTime.Parse(str);
      return result;
   } 
   
   public static string ConvertDateTimeToString(DateTime dateTime)
   {
      var result = dateTime.ToString("dd-mm-yyyy");
      return result;
   }

   public static TimeSpan GetDurationFromDateTimes(DateTime startTime, DateTime endTime)
   {
      var duration = endTime - startTime;
      return duration;
   }

   public static TimeSpan ConvertStringToTimeSpan(string str)
   {
      return TimeSpan.Parse(str);
   }

   public static string GetReadableFormatFromDateTime(DateTime dateTime)
   {
      var formattedDate = dateTime.ToString("MMM d, yy - HH:mm:ss");
      return formattedDate;
   }
}