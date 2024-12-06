using mars_rover.InputHandlers;
using mars_rover.Grids;
using mars_rover.UI;

namespace mars_rover.RoverHandlers;

public class Rover : IMappable
{

    public Position CurrentPosition { get; private set; } 
    private Grid Grid { get; set; } 

    public Rover(Position startingPosition, Grid grid)
    {
        CurrentPosition = startingPosition;
        Grid = grid;
    }

    public bool PerformInstructions(Instruction[] instructions)
    {
        Position nextPosition = new Position(CurrentPosition.X, CurrentPosition.Y, CurrentPosition.Facing);
        foreach (Instruction instruction in instructions)
        {
            if (instruction == Instruction.Move)
            {
                nextPosition = moveForward(nextPosition);
                if (!Grid.CheckPosition(nextPosition).Item1)
                {
                    return false;
                }
            }
            else if (instruction == Instruction.TurnLeft || instruction == Instruction.TurnRight)
            {
                nextPosition = Turn(nextPosition, instruction);
            }
        }
        CurrentPosition = nextPosition;
        Console.WriteLine($"Current position: {CurrentPosition}");
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

    private Position moveForward(Position position)
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
}
