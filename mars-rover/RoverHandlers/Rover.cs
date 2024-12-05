using mars_rover.InputHandlers;
namespace mars_rover.RoverHandlers;

public class Rover(Position startingPosition)
{

    public Position Position { get; private set; } = startingPosition;

    public void PerformInstructions(Instruction[] instructions)
    {
        foreach (Instruction instruction in instructions)
        {
            if (instruction == Instruction.Move) Move();
            else if (instruction == Instruction.TurnLeft || instruction == Instruction.TurnRight) Turn(instruction);
        }

        Console.WriteLine("Current position: " + GetCurrentPosition());
    }
    public void Turn(Instruction instruction)
    {
        switch (Position.Facing)
        {
            case CardinalDirection.North:
                if (instruction == Instruction.TurnLeft) Position.Facing = CardinalDirection.West;
                else Position.Facing = CardinalDirection.East;
                break;
            case CardinalDirection.East:
                if (instruction == Instruction.TurnLeft) Position.Facing = CardinalDirection.North;
                else Position.Facing = CardinalDirection.South;
                break;
            case CardinalDirection.South:
                if (instruction == Instruction.TurnLeft) Position.Facing = CardinalDirection.East;
                else Position.Facing = CardinalDirection.West;
                break;
            case CardinalDirection.West:
                if (instruction == Instruction.TurnLeft) Position.Facing = CardinalDirection.South;
                else Position.Facing = CardinalDirection.North;
                break;
        }
    }

    public void Move()
    {
        switch (Position.Facing)
        {
            case CardinalDirection.North:
                if(canMove()) Position.y++;
                break;
            case CardinalDirection.South:
                if (canMove()) Position.y--;
                break;
            case CardinalDirection.East:
                if (canMove()) Position.X++;
                break;
            case CardinalDirection.West:
                if (canMove()) Position.X--;
                break;
        };
    }
    public string GetCurrentPosition()
    {
        return $"{Position.X} {Position.y} {Position.Facing}";
    }

    private bool canMove()
    {
        return true;
    }
}
