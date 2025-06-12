using System.Configuration;
using CodingTracker.DTO;
using CodingTracker.Models;
using CodingTracker.Utility;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Repositories;

public class SessionRepository
{
    private SqliteConnection _connection;
    private string _selectAllSql = Constants.AppConstants.SelectAllSessionsSql;


    public SessionRepository()
    {
        SetConnection();
    }


    public void AddSession(DateTime startTime, DateTime endTime)
    {
        var duration = DateTimeHelper.GetDurationFromDateTimes(startTime, endTime);
        if (duration.Days > 0)
        {
            throw new ArgumentOutOfRangeException(nameof(duration),
                "The duration of a coding session cannot be an entire day or more. If this wasn't a mistake, touch grass.");
        }

        _connection.Execute(
            "INSERT INTO coding_session (start_time, end_time, duration) VALUES (@startTime, @endTime, @duration)",
            new { startTime, endTime, duration });
    }

    public List<CodingSessionDTO> GetAllSessions()
    {
        var allSessions = _connection.Query<CodingSession>(_selectAllSql);
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
        var duration = DateTimeHelper.GetDurationFromDateTimes(startTime, endTime);
        _connection.Execute(
            "UPDATE coding_session SET start_time=@startTime, end_time=@endTime, duration=@duration WHERE id=@id",
            new { startTime, endTime, duration, id });
    }

    public void RemoveSession(int id)
    {
        _connection.Execute("DELETE FROM coding_session WHERE id=@id", new { id });
    }

    private void SetConnection()
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
        var relativeDbPath = ConfigurationManager.AppSettings.Get("DatabasePath");
        var absoluteDbPath = Path.Combine(projectRoot, relativeDbPath);

        var connectionString = $"Data Source={absoluteDbPath}";
        _connection = new SqliteConnection(connectionString);
    }
}