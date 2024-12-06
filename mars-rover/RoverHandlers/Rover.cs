using mars_rover.InputHandlers;
using mars_rover.Grids;
using mars_rover.UI;

namespace mars_rover.RoverHandlers;

public class Rover : IMappable
{

    public Position CurrentPosition { get; private set; } 
    private Grid Grid { get; set; } 
    private ConsoleUI ui;

    public Rover(Position startingPosition, Grid grid)
    {
        CurrentPosition = startingPosition;
        Grid = grid;
        ui = ConsoleUI.GetInstance();
    }

    public bool PerformInstructions(Instruction[] instructions)
    {
        Position nextPosition = new Position(CurrentPosition.X, CurrentPosition.Y, CurrentPosition.Facing);
        foreach (Instruction instruction in instructions)
        {
            if (instruction == Instruction.Move)
            {
                nextPosition = MoveForward(nextPosition);
                if (!CheckPosition(nextPosition))
                {
                    ui.DrawGrid(Grid, this);
                    return false;
                }
            }
            else if (instruction == Instruction.TurnLeft || instruction == Instruction.TurnRight)
            {
                nextPosition = Turn(nextPosition, instruction);
            }
        }
        CurrentPosition = nextPosition;
        Console.WriteLine("Current position: " + GetCurrentPosition());
        ui.DrawGrid(Grid, this);
        return true;
    }
    private Position Turn(Position position, Instruction instruction)
    {
        switch (position.Facing)
        {
            case CardinalDirection.North:
                if (instruction == Instruction.TurnLeft) position.Facing = CardinalDirection.West;
                else position.Facing = CardinalDirection.East;
                break;
            case CardinalDirection.East:
                if (instruction == Instruction.TurnLeft) position.Facing = CardinalDirection.North;
                else position.Facing = CardinalDirection.South;
                break;
            case CardinalDirection.South:
                if (instruction == Instruction.TurnLeft) position.Facing = CardinalDirection.East;
                else position.Facing = CardinalDirection.West;
                break;
            case CardinalDirection.West:
                if (instruction == Instruction.TurnLeft) position.Facing = CardinalDirection.South;
                else position.Facing = CardinalDirection.North;
                break;
        }
        return position;
    }

    private bool CheckPosition(Position position)
    {
        if (position.X < 0 || position.X >= Grid.Width || position.Y < 0 || position.Y >= Grid.Length) return false;
        return true;
    }

    private Position MoveForward(Position position)
    {
        switch (position.Facing)
        {
            case CardinalDirection.North:
                position.Y++;
                break;

            case CardinalDirection.South:
                position.Y--;
                break;

            case CardinalDirection.East:
                position.X++;
                break;

            case CardinalDirection.West:
                position.X--;
                break;
        }; return position;
    }
    public string GetCurrentPosition()
    {
        return $"{CurrentPosition.X} {CurrentPosition.Y} {CurrentPosition.Facing}";
    }
}
