using mars_rover.InputHandlers;
using System.ComponentModel.Design;
namespace mars_rover.RoverHandlers;

public class Rover(Position startingPosition, Grid grid)
{

    public Position CurrentPosition { get; private set; } = startingPosition;
    private Grid Grid { get; set; } = grid;

    public bool PerformInstructions(Instruction[] instructions)
    {
        foreach (Instruction instruction in instructions)
        {
            if (instruction == Instruction.Move)
            {
                Position nextPosition = MoveForward(CurrentPosition);
                if (CheckPosition(nextPosition)) CurrentPosition = nextPosition;
                else return false;            
            }
            else if (instruction == Instruction.TurnLeft || instruction == Instruction.TurnRight) Turn(instruction);
        }

        Console.WriteLine("Current position: " + GetCurrentPosition());
        return true;
    }
    private void Turn(Instruction instruction)
    {
        switch (CurrentPosition.Facing)
        {
            case CardinalDirection.North:
                if (instruction == Instruction.TurnLeft) CurrentPosition.Facing = CardinalDirection.West;
                else CurrentPosition.Facing = CardinalDirection.East;
                break;
            case CardinalDirection.East:
                if (instruction == Instruction.TurnLeft) CurrentPosition.Facing = CardinalDirection.North;
                else CurrentPosition.Facing = CardinalDirection.South;
                break;
            case CardinalDirection.South:
                if (instruction == Instruction.TurnLeft) CurrentPosition.Facing = CardinalDirection.East;
                else CurrentPosition.Facing = CardinalDirection.West;
                break;
            case CardinalDirection.West:
                if (instruction == Instruction.TurnLeft) CurrentPosition.Facing = CardinalDirection.South;
                else CurrentPosition.Facing = CardinalDirection.North;
                break;
        }
    }

    private bool CheckPosition(Position position)
    {
        if (position.X < 0 || position.X > Grid.Width || position.Y < 0 || position.Y > Grid.Length) return false;
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
