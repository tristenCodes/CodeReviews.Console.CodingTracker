using CodingTracker.Models;
using CodingTracker.Utility;
using Spectre.Console;

namespace CodingTracker.Controllers;

public static class MenuController
{
    public static string GetMainMenuSelection()
    {
        AnsiConsole.Clear();
        var menuSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>().Title("What would you like to do?").AddChoices(new[]
            {
                "Add coding session entry", "View coding session entries", "Update coding session entry",
                "Remove coding session entry"
            }));


        return menuSelection;
    }

    public static (DateTime startTime, DateTime endTime, TimeSpan duration) AddCodingSession()
    {
        AnsiConsole.Clear();
        var startTime =
            AnsiConsole.Prompt(
                new TextPrompt<string>("When did you [lime]start[/]? Must be in MM/DD/YYYY HH:MM:SS FORMAT").Validate(
                    Validation.ValidateDateTimeInput));

        var endTime =
            AnsiConsole.Prompt(
                new TextPrompt<string>("When did you [maroon]end?[/] Must be in MM/DD/YYYY HH:MM:SS FORMAT").Validate(
                    Validation.ValidateDateTimeInput));
        
        var startTimeDateTime = DateTimeHelper.ConvertStringToDateTime(startTime);
        var endTimeDateTime = DateTimeHelper.ConvertStringToDateTime(endTime);
        var duration = DateTimeHelper.GetDurationFromDateTimes(startTimeDateTime, endTimeDateTime);
        
        return (startTimeDateTime, endTimeDateTime, duration);
    }


    public static void UpdateCodingSession()
    {
        
    }

    public static void ViewCodingSessions(List<CodingSession> sessions)
    {
        AnsiConsole.Clear();
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        foreach (var session in sessions)
        {
            table.AddRow(session.Id.ToString(), DateTimeHelper.GetReadableFormatFromString(session.StartTime) ?? "N/A", DateTimeHelper.GetReadableFormatFromString(session.EndTime) ?? "N/A", session.Duration ?? "N/A");
        }
        
        AnsiConsole.Write(table);
    }

    public static void RemoveCodingSession()
    {
        throw new NotImplementedException();
    }
}