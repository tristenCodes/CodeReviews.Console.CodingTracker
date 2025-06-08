using System.Configuration;
using CodingTracker.Constants;
using CodingTracker.Models;
using CodingTracker.Repositories;
using CodingTracker.Utility;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker.Controllers;

public class SessionController
{
    private SessionRepository sessionRepository;

    public SessionController()
    {
        sessionRepository = new SessionRepository();
    }

    public void AddCodingSession()
    {
        var (startTime, endTime) = MenuController.AddCodingSession();
        sessionRepository.AddSession(startTime, endTime);
    }

    public void ViewCodingSessions()
    {
        var allSessions = sessionRepository.GetAllSessions();
        MenuController.ViewCodingSessions(allSessions);
    }

    public void UpdateCodingSession()
    {
        var sessions = sessionRepository.GetAllSessions();
        var (sessionId, newStartTime, newEndTime) = MenuController.UpdateCodingSession(sessions);
        var duration = DateTimeHelper.GetDurationFromDateTimes(newStartTime, newEndTime);

        sessionRepository.UpdateSession(sessionId, newStartTime, newEndTime);
        MenuController.DisplayUpdateSuccess(sessionId);
    }

    public void RemoveCodingSession()
    {
        var sessions = sessionRepository.GetAllSessions();
        var selectedSessionId = MenuController.RemoveCodingSession(sessions);
        
        sessionRepository.RemoveSession(selectedSessionId);
        AnsiConsole.MarkupLine($"[lime]Session with ID {selectedSessionId} deleted successfully[/]");
        Console.ReadKey();
    }

    public void StartStopwatchSession()
    {
        DateTime start = DateTime.Now;

        MenuController.StopwatchSession();

        DateTime end = DateTime.Now;

        var duration = DateTimeHelper.GetDurationFromDateTimes(start, end);
        MenuController.DisplayStopwatchSessionTimeSpan(duration);
        
        sessionRepository.AddSession(start, end);
        AnsiConsole.MarkupLine("Coding session saved to database.");
        AnsiConsole.MarkupLine("[grey]Press any key to continue...[/]");
        Console.ReadKey();
    }

}
