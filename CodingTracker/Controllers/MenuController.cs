using System.Runtime.CompilerServices;
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
        var (startTime, endTime) = MenuController.GetStartAndEndTime();
        var startTimeDateTime = DateTimeHelper.ConvertStringToDateTime(startTime);
        var endTimeDateTime = DateTimeHelper.ConvertStringToDateTime(endTime);
        var duration = DateTimeHelper.GetDurationFromDateTimes(startTimeDateTime, endTimeDateTime);

        return (startTimeDateTime, endTimeDateTime, duration);
    }

    public static (int id, DateTime startTime, DateTime endTime) UpdateCodingSession(List<CodingSession> sessions)
    {
        AnsiConsole.Clear();
        var selectedSession = AnsiConsole.Prompt(
            new SelectionPrompt<CodingSession>()
                .Title("Select a session to delete")
                .AddChoices(sessions)
                .UseConverter(session =>
                    $"ID: {session.Id} | Start: {session.StartTime} - End: {session.EndTime} | Duration: {session.Duration}"));

        int id = selectedSession.Id;
        var (newStartTime, newEndTime) = GetStartAndEndTime();

        return (id, DateTimeHelper.ConvertStringToDateTime(newStartTime),
            DateTimeHelper.ConvertStringToDateTime(newEndTime));
    }

    public static void ViewCodingSessions(List<CodingSession> sessions)
    {
        AnsiConsole.Clear();
        GenerateTableOfAllSessions(sessions);

        Console.ReadKey();
    }

    public static int RemoveCodingSession(List<CodingSession> sessions)
    {
        var selectedSession = AnsiConsole.Prompt(new SelectionPrompt<CodingSession>().Title("Select an entry to delete")
            .AddChoices(sessions)
            .UseConverter(s =>
                $"ID: {s.Id} | Start: {s.StartTime} - End: {s.EndTime} | Duration: {s.Duration}"));

        return selectedSession.Id;
    }

    public static void GenerateTableOfAllSessions(List<CodingSession> sessions)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");


        foreach (var session in sessions)
        {
            table.AddRow(session.Id.ToString(), DateTimeHelper.GetReadableFormatFromString(session.StartTime) ?? "N/A",
                DateTimeHelper.GetReadableFormatFromString(session.EndTime) ?? "N/A", session.Duration ?? "N/A");
        }

        AnsiConsole.Write(table);
    }

    private static (string startTime, string endTime) GetStartAndEndTime()
    {
        var startTime =
            AnsiConsole.Prompt(
                new TextPrompt<string>("When did you [lime]start[/]? Must be in MM/DD/YYYY HH:MM:SS FORMAT").Validate(
                    Validation.ValidateDateTimeInput));

        var endTime =
            AnsiConsole.Prompt(
                new TextPrompt<string>("When did you [maroon]end?[/] Must be in MM/DD/YYYY HH:MM:SS FORMAT").Validate(
                    Validation.ValidateDateTimeInput));
        return (startTime, endTime);
    }
}