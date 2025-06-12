# Coding Tracker

Coding Tracker is a way to track your time spent coding via the CLI. Enter a start time and end time or start and stop the stopwatch to track your time.

## Purpose

The purpose of this project was to incorporate a strong separation of concerns. I attempted to make the code relatively uncoupled and independent.

## Project Requirements

- show the data on the console, using the "Spectre.Console" library.
- tells the user the specific format you want the date and time to be logged and not allow any other format.
- contains a configuration file that you'll contain your database path and connection strings.
- coding duration is calculated based on the Start and End times, in a separate "CalculateDuration" method.
- user is able to input the start and end times manually.
- uses Dapper ORM for the data access instead of ADO.NET.
- when reading from the database, does not use an anonymous object, reads table into a List of Coding Sessions.
- follows the DRY Principle, and avoid code repetition.

## Technologies Used

- [.NET Core](https://learn.microsoft.com/en-us/dotnet/)
- [Spectre Console](https://spectreconsole.net/)
- [SQLite](https://www.sqlite.org/)
- [Dapper](https://www.learndapper.com/)
