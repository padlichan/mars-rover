using mars_rover.InputHandlers;
using mars_rover.RoverHandlers;
using mars_rover.Grids;
using mars_rover.UI;
using System.Runtime.InteropServices;


namespace mars_rover;

public class MissionControl
{
    private static MissionControl? instance;
    private Grid? grid;
    private List<Rover> rovers;
    private Rover? rover;
    private MissionControl()
    {
        rovers = [];
        Init();
        grid = SetMissionArea();
    }

    public static MissionControl GetInstance()
    {
        if (instance == null) instance = new MissionControl();
        return instance;
    }

    private void Init()
    {
        ConsoleUI.DisplayMessage("Welcome to Mars Rover!");

    }

    public void LaunchMission()
    {
        MainMenu.EnterMainMenu();

        Position startingPosition = SetStartingPosition();

        string name = SetRoverName();

        rover = AddRoverAtPosition(startingPosition, name);

        ControlRover();

    }

    private Rover AddRoverAtPosition(Position position, string name)
    {
        rover = new Rover(position, name);
        grid.Add(rover);
        ConsoleUI.DisplayMessage("Delivering rover to starting position...SUCCESS");
        rovers.Add(rover);
        ConsoleUI.DrawGrid(grid);
        return rover;
    }

    public void UpdateRoverPosition(Rover rover, Position oldPosition)
    {
        grid.Remove(oldPosition);
        grid.Add(rover);
    }

    public bool CheckPosition(Position position)
    {
        ConsoleUI.DisplayMessage("Checking grid position...");
        if (grid != null)
        {
            (bool, GridCheckOutcome) outcome = grid.CheckPosition(position);
            ConsoleUI.DisplayMessage(outcome.Item2.ToString());
            return outcome.Item1;
        }
        ConsoleUI.DisplayMessage("Grid does not exist");
        return false;
    }

    private string SetRoverName()
    {
        string name;
        do
        {
            name = Input.GetValidInput("Enter unique rover name: ");
        }
        while (rovers.Any(r => r.Name == name));
        return name;
    }

    private Grid SetMissionArea()
    {
        Grid missionArea;
        string gridInput;
        do
        {
            gridInput = Input.GetValidInput("Enter valid grid size (e.g. 5 5): ");
        }
        while (!Input.TryParseGrid(gridInput, out missionArea));
        ConsoleUI.DisplayMessage("Creating grid... SUCCESS");
        return missionArea;
    }

    private Position SetStartingPosition()
    {
        string positionInput;
        Position position;
        do
        {
            positionInput = Input.GetValidInput("Enter valid starting position (e.g. 0 0 E):");
        }
        while (!Input.TryParsePosition(positionInput, out position) || !CheckPosition(position));
        return position;
    }

    private void ControlRover()
    {
        do
        {
            Instruction[]? instructions = GetInstructions();
            if (instructions == null) break;
            bool hasPerformedInstructions = SendInstructions(rover, instructions);
            if (hasPerformedInstructions) ConsoleUI.DisplayMessage("Performing instructions... SUCCESS");
            else ConsoleUI.DisplayMessage("Performing instructions... FAIL");
            ConsoleUI.DrawGrid(grid);
        }
        while (true);
    }

    private static Instruction[]? GetInstructions()
    {
        string instructionsInput;
        Instruction[] instructions;
        do
        {
            instructionsInput = Input.GetValidInput("Enter valid rover instructions (e.g. LMMRMMR) OR Back to Menu (B): ");
            if (instructionsInput == "B") return null;
        }
        while (!Input.TryParseInstructions(instructionsInput, out instructions));
        return instructions;
    }

    private bool SendInstructions(Rover rover, Instruction[] instructions)
    {
        bool isSuccess = rover.PerformInstructions(instructions);
        Console.WriteLine($"{rover.Name}: {rover.CurrentPosition}");
        return isSuccess;
    }

    private Rover ChooseRover()
    {
        ListRovers();
        string roverName;
        do
        {
            roverName = Input.GetValidInput("Choose a rover: ");
        }
        while (!rovers.Any(r => r.Name == roverName));
        return rovers.Where(r => r.Name == roverName).First();
    }

    public void AddRover()
    {
        Position startPosition = SetStartingPosition();
        string name = SetRoverName();
        AddRoverAtPosition(startPosition, name);
    }

    public void ListRovers()
    {
        if (!HasAvailableRover()) return;
        
        ConsoleUI.DisplayMessage("Available rovers:");
        foreach (Rover rover in rovers)
        {
            ConsoleUI.DisplayMessage($"{rover.Name} - {rover.CurrentPosition}");
        }
        ConsoleUI.DrawGrid(grid);
    }

    public void RemoveRover()
    {
        if (!HasAvailableRover()) return;
        Rover rover = ChooseRover();
        rovers.Remove(rover);
        grid.Remove(rover.CurrentPosition);
        ConsoleUI.DisplayMessage($"{rover.Name} removed");
    }

    private bool HasAvailableRover()
    {
        if (rovers.Count == 0)
        {
            ConsoleUI.DisplayMessage("No available rovers. Try adding a rover");
            return false;
        }
        return true;
    }
}
