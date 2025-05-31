# Coding Tracker

Coding Tracker is a way to track your time spent coding via the CLI. Enter a start time and end time or start and stop the stopwatch to track your time.

## Project Requirements

- show the data on the console, you should use the "Spectre.Console" library.
- should tell the user the specific format you want the date and time to be logged and not allow any other format.
- contain a configuration file that you'll contain your database path and connection strings.
- coding duration should be calculated based on the Start and End times, in a separate "CalculateDuration" method.
- user should be able to input the start and end times manually.
- use Dapper ORM for the data access instead of ADO.NET.
- when reading from the database, must not use an anonymous object, read your table into a List of Coding Sessions.
- follow the DRY Principle, and avoid code repetition.

## Technologies

- [.NET CORE](https://learn.microsoft.com/en-us/dotnet/)
- [Spectre Console](https://spectreconsole.net/)
- [SQLite](https://www.sqlite.org/)
- [Dapper](https://www.learndapper.com/)
