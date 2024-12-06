using FluentAssertions;
using mars_rover.Grids;
using mars_rover.InputHandlers;
using mars_rover.RoverHandlers;

namespace mars_rover_test;

public class RoverTest
{


    [Test]
    [TestCase(Instruction.TurnRight, CardinalDirection.North, CardinalDirection.East)]
    [TestCase(Instruction.TurnLeft, CardinalDirection.North, CardinalDirection.West)]
    [TestCase(Instruction.TurnRight, CardinalDirection.East, CardinalDirection.South)]
    [TestCase(Instruction.TurnLeft, CardinalDirection.East, CardinalDirection.North)]
    [TestCase(Instruction.TurnRight, CardinalDirection.South, CardinalDirection.West)]
    [TestCase(Instruction.TurnLeft, CardinalDirection.South, CardinalDirection.East)]
    [TestCase(Instruction.TurnRight, CardinalDirection.West, CardinalDirection.North)]
    [TestCase(Instruction.TurnLeft, CardinalDirection.West, CardinalDirection.South)]

    public void PerformInstructions_RoverTurnsInCorrectDirection(Instruction instruction, CardinalDirection startDirection, CardinalDirection expectedResult)
    {
        Instruction[] instructions = { instruction };   
        Grid grid = new(5, 5);
        Position startPosition = new(0, 0, startDirection);
        Rover rover = new(startPosition, grid);

        bool isSuccess = rover.PerformInstructions(instructions);

        isSuccess.Should().BeTrue();
        rover.CurrentPosition.Facing.Should().Be(expectedResult);
    }

    [Test]
    [TestCase(CardinalDirection.North, 2, 3)]
    [TestCase(CardinalDirection.East, 3, 2)]
    [TestCase(CardinalDirection.South, 2, 1)]
    [TestCase(CardinalDirection.West, 1, 2)]
    public void PerformInstructions_RoverMovesOneSpaceForward(CardinalDirection direction, int expectedX, int expectedY)
    {
        Instruction[] instructions = { Instruction.Move };
        Grid grid = new(5, 5);
        Position startPosition = new(2, 2, direction);
        Rover rover = new(startPosition, grid);
        Position expectedResult = new Position(expectedX, expectedY, direction);

        bool isSuccess = rover.PerformInstructions(instructions);

        isSuccess.Should().BeTrue();
        rover.CurrentPosition.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void PerformInstructions_ComplexValidInput()
    {
        Instruction[] instructions = {Instruction.TurnLeft, Instruction.Move,
                                      Instruction.TurnLeft, Instruction.Move,
                                      Instruction.TurnLeft, Instruction.Move,
                                      Instruction.TurnLeft, Instruction.Move,
                                      Instruction.Move};

        Position expectedResult = new (1, 3, CardinalDirection.North);
        Grid grid = new (5, 5);
        Position startPosition = new (1, 2, CardinalDirection.North);
        Rover rover = new (startPosition, grid);
        
        bool isSuccess = rover.PerformInstructions(instructions);

        isSuccess.Should().BeTrue();
        rover.CurrentPosition.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void PerformInstructions_IgnoreFatalInstructions()
    {
        Instruction[] instructions = { Instruction.Move, Instruction.Move, Instruction.Move, Instruction.Move };
        Grid grid = new(3, 3);
        Position startPosition = new(0, 0, CardinalDirection.North);
        Rover rover = new(startPosition, grid);

        bool result = rover.PerformInstructions(instructions);

        result.Should().BeFalse();
    }
}
