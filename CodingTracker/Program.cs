using System.Configuration;
using CodingTracker.Controllers;
using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
var relativeDbPath = ConfigurationManager.AppSettings.Get("DatabasePath");
var absoluteDbPath = Path.Combine(projectRoot, relativeDbPath);

var connectionString = $"Data Source={absoluteDbPath}";

using var connection = new SqliteConnection(connectionString);

var sql = "SELECT id, start_time as StartTime, end_time as EndTime, duration FROM coding_session;";
var result = connection.Query<CodingSession>(sql).ToList();

var mainMenuSelection = MenuController.GetMainMenuSelection();

switch (mainMenuSelection)
{
    case "Add coding session entry":
        MenuController.AddCodingSession();
        break;
    case "Update a coding session entry":
        MenuController.UpdateCodingSession();
        break;
    case "View all coding session entries":
        MenuController.ViewCodingSessions();
        break;
    case "Remove a coding session entry":
        MenuController.RemoveCodingSession();
        break;
    default:
        throw new Exception("Invalid menu selection");
}
