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
        try
        {
            var (startTime, endTime) = MenuController.AddCodingSession();
            sessionRepository.AddSession(startTime, endTime);
        }
        catch (ArgumentOutOfRangeException e)
        {
            MenuController.DisplayArgumentOutOfRangeError(e);
        }
        finally
        {
            MenuController.DisplayPressAnyKeyPrompt();
        }
    }

    public void ViewCodingSessions()
    {
        try
        {
            var allSessions = sessionRepository.GetAllSessions();
            MenuController.ViewCodingSessions(allSessions);
        }
        catch (InvalidOperationException e)
        {
            MenuController.DisplayNoSessionError();
        }
        finally
        {
            MenuController.DisplayPressAnyKeyPrompt();
        }
    }

    public void UpdateCodingSession()
    {
        try
        {
            var sessions = sessionRepository.GetAllSessions();
            var (sessionId, newStartTime, newEndTime) = MenuController.UpdateCodingSession(sessions);
            var duration = DateTimeHelper.GetDurationFromDateTimes(newStartTime, newEndTime);

            sessionRepository.UpdateSession(sessionId, newStartTime, newEndTime);
            MenuController.DisplayUpdateSuccess(sessionId);
        }
        catch (InvalidOperationException e)
        {
            MenuController.DisplayNoSessionError();
        }
        finally
        {
            MenuController.DisplayPressAnyKeyPrompt();
        }
    }

    public void RemoveCodingSession()
    {
        try
        {
            var sessions = sessionRepository.GetAllSessions();
            var selectedSessionId = MenuController.RemoveCodingSession(sessions);

            sessionRepository.RemoveSession(selectedSessionId);
            MenuController.DisplayRemoveSuccess(selectedSessionId);
        }
        catch (InvalidOperationException e)
        {
            MenuController.DisplayNoSessionError();
        }
        finally
        {
            MenuController.DisplayPressAnyKeyPrompt();
        }
    }

    public void StartStopwatchSession()
    {
        DateTime start = DateTime.Now;

        MenuController.StartStopwatchSession();

        DateTime end = DateTime.Now;

        var duration = DateTimeHelper.GetDurationFromDateTimes(start, end);
        MenuController.StopStopwatchSession(duration);

        sessionRepository.AddSession(start, end);
        AnsiConsole.MarkupLine("Coding session saved to database.");
        MenuController.DisplayPressAnyKeyPrompt();
    }
}