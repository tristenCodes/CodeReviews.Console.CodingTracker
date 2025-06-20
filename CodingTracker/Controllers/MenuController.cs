using CodingTracker.Constants;
using CodingTracker.DTO;
using CodingTracker.Utility;
using Spectre.Console;

namespace CodingTracker.Controllers;

public static class MenuController
{
    public static string GetMainMenuSelection()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new FigletText("Coding Tracker")
                .LeftJustified()
                .Color(Color.Blue));

        var menuSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>().Title("What would you like to do?").AddChoices(new[]
            {
                "Stopwatch mode", "Add coding session entry", "View coding session entries",
                "Update coding session entry",
                "Remove coding session entry", "Exit"
            }));


        return menuSelection;
    }

    public static void StartStopwatchSession()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Stopwatch session started...[/]");
        AnsiConsole.MarkupLine("[grey]Press any key to stop the session.[/]");
        Console.ReadKey();
    }

    public static void StopStopwatchSession(TimeSpan timeSpan)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[yellow]Time spent coding: {timeSpan.ToString("hh\\:mm\\:ss")}[/]");
    }

    public static (DateTime startTime, DateTime endTime) AddCodingSession()
    {
        AnsiConsole.Clear();
        var (startTime, endTime) = MenuController.GetStartAndEndTime();
        var startTimeDateTime = DateTimeHelper.ConvertStringToDateTime(startTime);
        var endTimeDateTime = DateTimeHelper.ConvertStringToDateTime(endTime);

        return (startTimeDateTime, endTimeDateTime);
    }

    public static (int id, DateTime startTime, DateTime endTime) UpdateCodingSession(List<CodingSessionDto> sessions)
    {
        AnsiConsole.Clear();
        var selectedSession = AnsiConsole.Prompt(
            new SelectionPrompt<CodingSessionDto>()
                .Title("Select a session to update")
                .AddChoices(sessions)
                .UseConverter(session =>
                    $"ID: {session.Id} | Start: {DateTimeHelper.GetReadableFormatFromDateTime(session.StartTime)} - End: {DateTimeHelper.GetReadableFormatFromDateTime(session.EndTime)} | Duration: {session.Duration}"));

        var id = selectedSession.Id;
        var (newStartTime, newEndTime) = GetStartAndEndTime();

        return (id, DateTimeHelper.ConvertStringToDateTime(newStartTime),
            DateTimeHelper.ConvertStringToDateTime(newEndTime));
    }

    public static void ViewCodingSessions(List<CodingSessionDto> sessions)
    {
        AnsiConsole.Clear();
        GenerateTableOfAllSessions(sessions);
    }

    public static int RemoveCodingSession(List<CodingSessionDto> sessions)
    {
        AnsiConsole.Clear();
        var selectedSession = AnsiConsole.Prompt(new SelectionPrompt<CodingSessionDto>()
            .Title("Select an entry to delete")
            .AddChoices(sessions)
            .UseConverter(s =>
                $"ID: {s.Id} | Start: {DateTimeHelper.GetReadableFormatFromDateTime(s.StartTime)} - End: {DateTimeHelper.GetReadableFormatFromDateTime(s.EndTime)} | Duration: {s.Duration}"));

        return selectedSession.Id;
    }

    public static void DisplayUpdateSuccess(int sessionId)
    {
        AnsiConsole.MarkupLine($"[green]Session ID {sessionId} updated successfully[/]");
    }

    public static void DisplayRemoveSuccess(int sessionId)
    {
        AnsiConsole.MarkupLine($"[green]Session ID {sessionId} removed successfully[/]");
    }

    public static void DisplayNoSessionError()
    {
        AnsiConsole.MarkupLine("[maroon]No existing coding sessions found in database.[/]");
    }

    public static void DisplayPressAnyKeyPrompt()
    {
        AnsiConsole.MarkupLine(AppConstants.PressAnyKeyMarkup);
        Console.ReadKey();
    }

    public static void DisplayArgumentOutOfRangeError(ArgumentOutOfRangeException e)
    {
        AnsiConsole.MarkupLine($"[red]Exception occurred: {e.Message}[/]");
    }

    private static void GenerateTableOfAllSessions(List<CodingSessionDto> sessions)
    {
        if (sessions.Count == 0)
        {
            throw new InvalidOperationException("No sessions exist currently.");
        }
        else
        {
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Start Time");
            table.AddColumn("End Time");
            table.AddColumn("Duration");


            foreach (var session in sessions)
            {
                table.AddRow(session.Id.ToString(),
                    DateTimeHelper.GetReadableFormatFromDateTime(session.StartTime) ?? "N/A",
                    DateTimeHelper.GetReadableFormatFromDateTime(session.EndTime) ?? "N/A",
                    session.Duration.ToString("hh\\:mm\\:ss"));
            }

            AnsiConsole.Write(table);
        }
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
