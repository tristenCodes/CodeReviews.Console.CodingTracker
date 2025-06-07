using CodingTracker.Controllers;

SessionController sessionController = new();

while (true)
{
    var mainMenuSelection = MenuController.GetMainMenuSelection();

    switch (mainMenuSelection)
    {
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
        default:
            throw new Exception("Invalid menu selection");
    }
}