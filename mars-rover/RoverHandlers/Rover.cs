using mars_rover.InputHandlers;
using mars_rover.Grids;
using mars_rover.UI;

namespace mars_rover.RoverHandlers;

public class Rover : IMappable
{

    public Position CurrentPosition { get; private set; } 
    private MissionControl missionControl { get; set; }

    public string Name { get; private set; }

    public Rover(Position startingPosition, string name)
    {
        CurrentPosition = startingPosition;
        Name = name;
        missionControl = MissionControl.GetInstance();
    }

    public bool PerformInstructions(Instruction[] instructions)
    {
        Position nextPosition = new Position(CurrentPosition.X, CurrentPosition.Y, CurrentPosition.Facing);
        foreach (Instruction instruction in instructions)
        {
            if (instruction == Instruction.Move)
            {
                nextPosition = moveForward(nextPosition);
                if (!missionControl.CheckPosition(nextPosition))
                {
                    return false;
                }
            }
            else if (instruction == Instruction.TurnLeft || instruction == Instruction.TurnRight)
            {
                nextPosition = Turn(nextPosition, instruction);
            }
        }
        Position oldPosition = new Position(CurrentPosition.X, CurrentPosition.Y, CurrentPosition.Facing);
        CurrentPosition = nextPosition;
        missionControl.UpdateRoverPosition(this, oldPosition);
        return true;
    }
    private Position Turn(Position position, Instruction instruction)
    {
        switch (position.Facing)
        {
            case CardinalDirection.NORTH:
                if (instruction == Instruction.TurnLeft) position.Facing = CardinalDirection.WEST;
                else position.Facing = CardinalDirection.EAST;
                break;
            case CardinalDirection.EAST:
                if (instruction == Instruction.TurnLeft) position.Facing = CardinalDirection.NORTH;
                else position.Facing = CardinalDirection.SOUTH;
                break;
            case CardinalDirection.SOUTH:
                if (instruction == Instruction.TurnLeft) position.Facing = CardinalDirection.EAST;
                else position.Facing = CardinalDirection.WEST;
                break;
            case CardinalDirection.WEST:
                if (instruction == Instruction.TurnLeft) position.Facing = CardinalDirection.SOUTH;
                else position.Facing = CardinalDirection.NORTH;
                break;
        }
        return position;
    }

    private Position moveForward(Position position)
    {
        switch (position.Facing)
        {
            case CardinalDirection.NORTH:
                position.Y++;
                break;

            case CardinalDirection.SOUTH:
                position.Y--;
                break;

            case CardinalDirection.EAST:
                position.X++;
                break;

            case CardinalDirection.WEST:
                position.X--;
                break;
        }; return position;
    }
}
