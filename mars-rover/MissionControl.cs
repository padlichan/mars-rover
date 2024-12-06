using mars_rover.InputHandlers;
using mars_rover.RoverHandlers;
using mars_rover.Grids;
using mars_rover.UI;


namespace mars_rover
{
    public class MissionControl
    {
        private static MissionControl? instance;
        private Grid? grid;
        private List<Rover> rovers;
        private Rover? rover;
        private ConsoleUI ui;
        private MissionControl()
        {
            rovers = [];
            grid = null;
            rover = null;
            ui = ConsoleUI.GetInstance();
        }

        public static MissionControl GetInstance()
        {
            if (instance == null) instance = new MissionControl();
            return instance;
        }

        public void LaunchMission()
        {
            ui.DisplayMessage("Welcome to Mars Rover!");

            grid = PickMissionArea();
            ui.DisplayMessage("Creating grid... SUCCESS");

            Position startingPosition = PickStartingPosition();

            rover = AddRoverAtPosition(startingPosition);
            ui.DisplayMessage("Delivering rover to starting position...SUCCESS");
            ui.DrawGrid(grid, rover);

            do
            {
                Instruction[]? instructions = GetInstructions();
                if (instructions == null) break;
                bool hasPerformedInstructions = SendInstructions(rover, instructions);
                if (hasPerformedInstructions) ui.DisplayMessage("Performing instructions... SUCCESS");
                else ui.DisplayMessage("Performing instructions... FAIL");
                ui.DrawGrid(grid, rover);
            }
            while (true);
        }

        public Rover AddRoverAtPosition(Position position)
        {
            rover = new Rover(position, grid);
            grid.Add(rover, position);
            return rover;
        }

        public bool CheckPosition(Position position)
        {
            ui.DisplayMessage("Checking grid position...");
            if (grid != null)
            {
                (bool, GridCheckOutcome) outcome = grid.CheckPosition(position);
                ui.DisplayMessage(outcome.Item2.ToString());
                return outcome.Item1;
            }
            ui.DisplayMessage("Grid does not exist");
            return false;
        }

        public Grid PickMissionArea()
        {
            //Prompt user to input plateau size
            Grid missionArea;
            string gridInput;
            do
            {
                gridInput = Input.GetInput("Enter valid grid size (e.g. 5 5): ");
            }
            while (!Input.TryParseGrid(gridInput, out missionArea));
            return missionArea;
        }

        public Position PickStartingPosition()
        {
            //Prompt user to input starting position
            string positionInput;
            Position position;
            do
            {
                positionInput = Input.GetInput("Enter valid starting position (e.g. 0 0 E):");
            }
            while (!Input.TryParsePosition(positionInput, out position) || !CheckPosition(position));
            return position;           
        }

        public Instruction[]? GetInstructions()
        {
            string instructionsInput;
            Instruction[] instructions;
            do
            {
                instructionsInput = Input.GetInput("Enter valid rover instructions (e.g. LMMRMMR) OR Quit (Q): ");
                if (instructionsInput == "Q") return null;
            }
            while (!Input.TryParseInstructions(instructionsInput, out instructions));
            return instructions;
        }

        public bool SendInstructions(Rover rover, Instruction[] instructions)
        {
            return rover.PerformInstructions(instructions);
        }
    }
}
