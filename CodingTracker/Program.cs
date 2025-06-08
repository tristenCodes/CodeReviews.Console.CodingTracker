using CodingTracker.Controllers;
using Spectre.Console;

SessionController sessionController = new();

bool continueLoop = true;
while (continueLoop)
{
    var mainMenuSelection = MenuController.GetMainMenuSelection();

    switch (mainMenuSelection)
    {
        case "Stopwatch mode":
            sessionController.StartStopwatchSession();
            break;
        case "Add coding session entry":
            sessionController.AddCodingSession();
            break;
        case "Update coding session entry":
            sessionController.UpdateCodingSession();
            break;
        case "View coding session entries":
            sessionController.ViewCodingSessions();
            break;
        case "Remove coding session entry":
            sessionController.RemoveCodingSession();
            break;
        case "Exit":
            continueLoop = false;
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Bye!")
                    .LeftJustified()
                    .Color(Color.Blue));
            break;
        default:
            throw new Exception("Invalid menu selection");
    }
}