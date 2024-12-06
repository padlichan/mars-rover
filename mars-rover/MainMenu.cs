using mars_rover.InputHandlers;
using mars_rover.RoverHandlers;

namespace mars_rover;

public static class MainMenu
{
    private static MissionControl missionControl = MissionControl.GetInstance();

    public static void EnterMainMenu()
    {
        do
        {
            string input;
            MenuChoice choice;
            do
            {
                input = Input.GetValidInput("A: Add rover, R: Remove rover, L: List rovers, C: Control Rover, Q: Quit\n");
            }
            while (!Input.TryParseMenuChoice(input, out choice));

            switch (choice)
            {
                case MenuChoice.QUIT: Environment.Exit(0); break;
                case MenuChoice.ADD_ROVER: missionControl.AddRover(); break;
                case MenuChoice.REMOVE_ROVER: missionControl.RemoveRover(); break;
                case MenuChoice.LIST_ROVERS: missionControl.ListRovers(); break;
                case MenuChoice.CONTROL_ROVER: missionControl.ControlRover(); break;

            }
        }
        while (true);
    }
}

public enum MenuChoice
{
    QUIT,
    ADD_ROVER,
    REMOVE_ROVER,
    LIST_ROVERS,
    CONTROL_ROVER,
    DEFAULT
}
