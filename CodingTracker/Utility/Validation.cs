using System.Text.RegularExpressions;
using Spectre.Console;

namespace CodingTracker.Utility;

public static class Validation
{
    public static ValidationResult ValidateDateTimeInput(string input)
    {
        var pattern =
            @"^(0[1-9]|1[0-2])\/(0[1-9]|[12]\d|3[01])\/\d{4}\s([01]\d|2[0-3]):([0-5]\d):([0-5]\d)$";
        var reg = new Regex(pattern);
        if (!reg.IsMatch(input))
            return ValidationResult.Error(
                "Your entry does not match the specified format. Please try again.");
        else return ValidationResult.Success();
    }
}