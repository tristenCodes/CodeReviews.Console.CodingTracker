using System.Configuration;
using CodingTracker.DTO;
using CodingTracker.Models;
using CodingTracker.Utility;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Repositories;

public class SessionRepository
{
    private string _connectionString;
    private string _selectAllSql = Constants.AppConstants.SelectAllSessionsSql;

    public SessionRepository()
    {
        SetConnectionString();
    }


    public void AddSession(DateTime startTime, DateTime endTime)
    {
        using var connection = new SqliteConnection(_connectionString);
        var duration = DateTimeHelper.GetDurationFromDateTimes(startTime, endTime);
        connection.Execute(
            "INSERT INTO coding_session (start_time, end_time, duration) VALUES (@startTime, @endTime, @duration)",
            new { startTime, endTime, duration });
    }

    public List<CodingSessionDTO> GetAllSessions()
    {
        using var connection = new SqliteConnection(_connectionString);
        var allSessions = connection.Query<CodingSession>(_selectAllSql);
        var allSessionsWithDateTimes = new List<CodingSessionDTO>();

        foreach (var codingSession in allSessions)
        {
            var startTimeDateTime = DateTimeHelper.ConvertStringToDateTime(codingSession.StartTime);
            var endTimeDateTime = DateTimeHelper.ConvertStringToDateTime(codingSession.StartTime);
            var duration = DateTimeHelper.ConvertStringToTimeSpan(codingSession.Duration);

            var sessionWithDateTime = new CodingSessionDTO
            {
                Id = codingSession.Id,
                StartTime = startTimeDateTime,
                EndTime = endTimeDateTime,
                Duration = duration
            };

            allSessionsWithDateTimes.Add(sessionWithDateTime);
        }

        return allSessionsWithDateTimes;
    }

    public void UpdateSession(int id, DateTime startTime, DateTime endTime)
    {
        using var connection = new SqliteConnection(_connectionString);
        var duration = DateTimeHelper.GetDurationFromDateTimes(startTime, endTime);
        connection.Execute("UPDATE coding_session SET start_time=@startTime, end_time=@endTime, duration=@duration");
    }

    public void RemoveSession(int id)
    {
    }
    
    private void SetConnectionString()
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
        var relativeDbPath = ConfigurationManager.AppSettings.Get("DatabasePath");
        var absoluteDbPath = Path.Combine(projectRoot, relativeDbPath);

        _connectionString = $"Data Source={absoluteDbPath}";
    }
}