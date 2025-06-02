using CodingTracker.Utility;
using Spectre.Console;

namespace CodingTracker.Controllers;

public static class MenuController
{
    public static string GetMainMenuSelection()
    {
        AnsiConsole.Clear();
        var menuSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>().Title("What would you like to do?").AddChoices(new[]
            {
                "Add coding session entry", "View coding session entries", "Update coding session entry",
                "Remove coding session entry"
            }));


        return menuSelection;
    }

    public static void AddCodingSession()
    {
        AnsiConsole.Clear();
        var startTime =
            AnsiConsole.Prompt(
                new TextPrompt<string>("When did you start? Must be in MM/DD/YYYY HH:MM:SS FORMAT").Validate(
                    Validation.ValidateDateTimeInput));

        var endTime =
            AnsiConsole.Prompt(
                new TextPrompt<string>("When did you end? Must be in MM/DD/YYYY HH:MM:SS FORMAT").Validate(
                    Validation.ValidateDateTimeInput));
    }


    public static void UpdateCodingSession()
    {
        throw new NotImplementedException();
    }

    public static void ViewCodingSessions()
    {
        throw new NotImplementedException();
    }

    public static void RemoveCodingSession()
    {
        throw new NotImplementedException();
    }
}