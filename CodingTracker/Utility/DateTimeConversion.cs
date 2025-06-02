namespace CodingTracker.Utility;

public static class DateTimeConversion
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
}