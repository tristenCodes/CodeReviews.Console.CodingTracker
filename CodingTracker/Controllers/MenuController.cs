using System.Runtime.CompilerServices;
using CodingTracker.DTO;
using CodingTracker.Models;
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

    public static void StopwatchSession()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Stopwatch session started...[/]");
        AnsiConsole.MarkupLine("[grey]Press any key to stop the session.[/]");
        Console.ReadKey();
    }

    public static void DisplayStopwatchSessionTimeSpan(TimeSpan timeSpan)
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

    public static (int id, DateTime startTime, DateTime endTime) UpdateCodingSession(List<CodingSessionDTO> sessions)
    {
        AnsiConsole.Clear();
        var selectedSession = AnsiConsole.Prompt(
            new SelectionPrompt<CodingSessionDTO>()
                .Title("Select a session to update")
                .AddChoices(sessions)
                .UseConverter(session =>
                    $"ID: {session.Id} | Start: {DateTimeHelper.GetReadableFormatFromDateTime(session.StartTime)} - End: {DateTimeHelper.GetReadableFormatFromDateTime(session.EndTime)} | Duration: {session.Duration}"));

        var id = selectedSession.Id;
        var (newStartTime, newEndTime) = GetStartAndEndTime();

        return (id, DateTimeHelper.ConvertStringToDateTime(newStartTime),
            DateTimeHelper.ConvertStringToDateTime(newEndTime));
    }

    public static void ViewCodingSessions(List<CodingSessionDTO> sessions)
    {
        AnsiConsole.Clear();
        GenerateTableOfAllSessions(sessions);

        Console.ReadKey();
    }

    public static int RemoveCodingSession(List<CodingSessionDTO> sessions)
    {
        var selectedSession = AnsiConsole.Prompt(new SelectionPrompt<CodingSessionDTO>()
            .Title("Select an entry to delete")
            .AddChoices(sessions)
            .UseConverter(s =>
                $"ID: {s.Id} | Start: {DateTimeHelper.GetReadableFormatFromDateTime(s.StartTime)} - End: {DateTimeHelper.GetReadableFormatFromDateTime(s.EndTime)} | Duration: {s.Duration}"));

        return selectedSession.Id;
    }

    public static void DisplayUpdateSuccess(int sessionId)
    {
        AnsiConsole.MarkupLine($"[green]Session ID {sessionId} updated successfully[/]");
        Console.ReadKey();
    }

    private static void GenerateTableOfAllSessions(List<CodingSessionDTO> sessions)
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
                DateTimeHelper.GetReadableFormatFromDateTime(session.EndTime) ?? "N/A", session.Duration.ToString("hh\\:mm\\:ss"));
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