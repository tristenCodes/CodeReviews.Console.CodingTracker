using CodingTracker.Controllers;

SessionController sessionController = new();

var mainMenuSelection = MenuController.GetMainMenuSelection();

switch (mainMenuSelection)
{
    case "Add coding session entry":
        sessionController.AddCodingSession();
        break;
    case "Update a coding session entry":
        sessionController.UpdateCodingSession();
        MenuController.UpdateCodingSession();
        break;
    case "View coding session entries":
        sessionController.ViewCodingSessions();
        break;
    case "Remove a coding session entry":
        MenuController.RemoveCodingSession();
        break;
    default:
        throw new Exception("Invalid menu selection");
}
