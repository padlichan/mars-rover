using mars_rover.InputHandlers;
using System.ComponentModel.Design;
namespace mars_rover.RoverHandlers;

public class Rover(Position startingPosition, Grid grid)
{

    public Position CurrentPosition { get; private set; } = startingPosition;
    private Grid Grid { get; set; } = grid;

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
                    DrawRover();
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
        DrawRover();
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

    public void DrawRover()
    {
        char c = CurrentPosition.Facing switch
        {
            CardinalDirection.North => '^',
            CardinalDirection.East => '>',
            CardinalDirection.South => 'v',
            CardinalDirection.West => '<',
            _ => ' '

        };
        for (int i = Grid.Length - 1; i >= 0; i--)
        {
            for (int j = 0; j < Grid.Width; j++)
            {
                if (i == CurrentPosition.Y && j == CurrentPosition.X) Console.Write(c);
                else Console.Write('_');
                Console.Write(' ');
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
