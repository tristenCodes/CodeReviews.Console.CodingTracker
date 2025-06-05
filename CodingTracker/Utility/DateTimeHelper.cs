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

   public static string GetReadableFormatFromDateTime(DateTime dateTime)
   {
      var formattedDate = dateTime.ToString("MM/dd/yyyy - HH:mm:ss");
      return formattedDate;
   }

   public static string GetReadableFormatFromString(string dateTimeString)
   {
     var dateTime = ConvertStringToDateTime(dateTimeString);
     var formattedDate = dateTime.ToString("MM/dd/yyyy - HH:mm:ss");
     return formattedDate;
   }
}