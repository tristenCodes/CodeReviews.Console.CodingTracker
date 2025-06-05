using System.Configuration;
using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;

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
        var sql = "INSERT INTO coding_session (start_time, end_time, duration) VALUES (@startTime, @endTime, @duration)";
        connection.Execute(sql, new { startTime, endTime, duration });
    }

    public void ViewCodingSessions()
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = "SELECT id, start_time as StartTime, end_time as EndTime, duration FROM coding_session";
        var sessions = connection.Query<CodingSession>(sql).ToList();
       MenuController.ViewCodingSessions(sessions); 
    }

    public void UpdateCodingSession()
    {
        MenuController.UpdateCodingSession();
    }

    public void RemoveCodingSession(int id)
    {
        throw new NotImplementedException();
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