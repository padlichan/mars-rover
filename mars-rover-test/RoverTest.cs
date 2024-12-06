using FluentAssertions;
using mars_rover;
using mars_rover.Grids;
using mars_rover.InputHandlers;
using mars_rover.RoverHandlers;

namespace mars_rover_test;

public class RoverTest
{


    [Test]
    [TestCase(Instruction.TurnRight, CardinalDirection.NORTH, CardinalDirection.EAST)]
    [TestCase(Instruction.TurnLeft, CardinalDirection.NORTH, CardinalDirection.WEST)]
    [TestCase(Instruction.TurnRight, CardinalDirection.EAST, CardinalDirection.SOUTH)]
    [TestCase(Instruction.TurnLeft, CardinalDirection.EAST, CardinalDirection.NORTH)]
    [TestCase(Instruction.TurnRight, CardinalDirection.SOUTH, CardinalDirection.WEST)]
    [TestCase(Instruction.TurnLeft, CardinalDirection.SOUTH, CardinalDirection.EAST)]
    [TestCase(Instruction.TurnRight, CardinalDirection.WEST, CardinalDirection.NORTH)]
    [TestCase(Instruction.TurnLeft, CardinalDirection.WEST, CardinalDirection.SOUTH)]

    public void PerformInstructions_RoverTurnsInCorrectDirection(Instruction instruction, CardinalDirection startDirection, CardinalDirection expectedResult)
    {
        Instruction[] instructions = { instruction };   
        Grid grid = new(5, 5);
        Position startPosition = new(0, 0, startDirection);
        Rover rover = new(startPosition, "name");

        bool isSuccess = rover.PerformInstructions(instructions);

        isSuccess.Should().BeTrue();
        rover.CurrentPosition.Facing.Should().Be(expectedResult);
    }

    [Test]
    [TestCase(CardinalDirection.NORTH, 2, 3)]
    [TestCase(CardinalDirection.EAST, 3, 2)]
    [TestCase(CardinalDirection.SOUTH, 2, 1)]
    [TestCase(CardinalDirection.WEST, 1, 2)]
    public void PerformInstructions_RoverMovesOneSpaceForward(CardinalDirection direction, int expectedX, int expectedY)
    {
        Instruction[] instructions = { Instruction.Move };
        Grid grid = new(5, 5);
        Position startPosition = new(2, 2, direction);
        Rover rover = new(startPosition, "name");
        Position expectedResult = new Position(expectedX, expectedY, direction);

        bool isSuccess = rover.PerformInstructions(instructions);

        rover.CurrentPosition.Should().BeEquivalentTo(expectedResult);
        isSuccess.Should().BeTrue();
    }

    [Test]
    public void PerformInstructions_ComplexValidInput()
    {
        //To Do: Moq missionControl
        Instruction[] instructions = {Instruction.TurnLeft, Instruction.Move,
                                      Instruction.TurnLeft, Instruction.Move,
                                      Instruction.TurnLeft, Instruction.Move,
                                      Instruction.TurnLeft, Instruction.Move,
                                      Instruction.Move};

        Position expectedResult = new (1, 3, CardinalDirection.NORTH);
        Grid grid = new (5, 5);

        Position startPosition = new (1, 2, CardinalDirection.NORTH);
        Rover rover = new (startPosition, "name");
        
        bool isSuccess = rover.PerformInstructions(instructions);

        isSuccess.Should().BeTrue();
        rover.CurrentPosition.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void PerformInstructions_IgnoreFatalInstructions()
    {
        Instruction[] instructions = { Instruction.Move, Instruction.Move, Instruction.Move, Instruction.Move };
        Grid grid = new(3, 3);
        Position startPosition = new(0, 0, CardinalDirection.NORTH);
        Rover rover = new(startPosition, "name");

        bool result = rover.PerformInstructions(instructions);

        result.Should().BeFalse();
    }
}
