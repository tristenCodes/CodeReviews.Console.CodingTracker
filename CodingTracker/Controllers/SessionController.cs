using System.Configuration;
using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using CodingTracker.Constants;
using CodingTracker.Utility;
using Spectre.Console;

namespace CodingTracker.Controllers;

public class SessionController
{
    private readonly string _connectionString;

    public SessionController()
    {
        _connectionString = GetConnectionString();
    }

    public void AddCodingSession()
    {
        var (startTime, endTime, duration) = MenuController.AddCodingSession();
        using var connection = new SqliteConnection(_connectionString);
        var sql =
            "INSERT INTO coding_session (start_time, end_time, duration) VALUES (@startTime, @endTime, @duration)";
        connection.Execute(sql, new { startTime, endTime, duration });
    }

    public void ViewCodingSessions()
    {
        using var connection = new SqliteConnection(_connectionString);
        var sessions = connection.Query<CodingSession>(AppConstants.SelectAllSessionsSql).ToList();
        MenuController.ViewCodingSessions(sessions);
    }

    public void UpdateCodingSession()
    {
        using var connection = new SqliteConnection(_connectionString);
        var sessions = connection.Query<CodingSession>(AppConstants.SelectAllSessionsSql).ToList();
        var (sessionId, newStartTime, newEndTime) = MenuController.UpdateCodingSession(sessions);
        var duration = DateTimeHelper.GetDurationFromDateTimes(newStartTime, newEndTime);
        connection.Execute(
            "UPDATE coding_session SET start_time = @newStartTime, end_time = @newEndTime, duration=@duration WHERE id=@sessionId",
            new { sessionId, newStartTime, newEndTime, duration });

        AnsiConsole.MarkupLine($"[green]Session ID {sessionId} updated successfully[/]");
        Console.ReadKey();
    }

    public void RemoveCodingSession()
    {
        using var connection = new SqliteConnection(_connectionString);
        var sessions = connection.Query<CodingSession>(AppConstants.SelectAllSessionsSql).ToList();
        var selectedSessionId = MenuController.RemoveCodingSession(sessions);
        connection.Execute($"DELETE FROM coding_session WHERE id=@selectedSessionId", new { selectedSessionId });
        AnsiConsole.MarkupLine($"[lime]Session with ID {selectedSessionId} deleted successfully[/]");
        Console.ReadKey();
    }

    private string GetConnectionString()
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
        var relativeDbPath = ConfigurationManager.AppSettings.Get("DatabasePath");
        var absoluteDbPath = Path.Combine(projectRoot, relativeDbPath);

        var connectionString = $"Data Source={absoluteDbPath}";

        return connectionString;
    }
}