using FluentAssertions;
using mars_rover.InputHandlers;
namespace mars_rover_test;

public class InputTest
{

    [Test]
    public void Input_NotNullOrEmpty()
    {
        //Assign
        string input = $"L{Environment.NewLine}";
        string expectedResult = "L";

        var stringReader = new StringReader(input);
        var originalConsoleIn = Console.In;
        Console.SetIn(stringReader);

        //Act
        Predicate<string> isValid = (x) => true;
        string result = Input.GetInput("");

        //Assert
        result.Should().Be(expectedResult);

        //Cleanup
        Console.SetIn(originalConsoleIn);
    }

    [Test]
    public void Input_EmptyInputIsIgnored()
    {
        // Arrange
        var simulatedInput = "\n\nM\n";
        var expectedOutput =
            $"Input Rover instructions: " +
            $"Input Rover instructions: " +
            $"Input Rover instructions: ";

        var expectedResult = "M";


        // Act
        var originalOut = Console.Out;
        var originalIn = Console.In;
        var stringReader = new StringReader(simulatedInput);
        Console.SetIn(stringReader);

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        var result = Input.GetInput("Input Rover instructions: ");

        // Assert
        result.Should().Be(expectedResult);
        expectedOutput.Should().Be(stringWriter.ToString());

        //Cleanup

        Console.SetOut(originalOut);
        Console.SetIn(originalIn);
    }
    [Test]
    public void ParsePlateauSize_ReturnTrueAndCorrectForValidPlateauSize()
    {
        //Arrange
        string input = "2 5";
        var expectedResult = new Grid(2, 5);

        //Act
        bool isSuccess = Input.TryParsePlateauSize(input, out Grid result);

        //Assert
        isSuccess.Should().BeTrue();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    [TestCase("0 0")]
    [TestCase("-1 0")]
    [TestCase("-1 -1")]
    [TestCase(" ")]
    [TestCase("ASDFGHJKL")]
    public void ParsePlateauSize_ReturnFalseForInvalidPlateauSize(string input)
    {
        //Arrange

        //Act
        bool result = Input.TryParsePlateauSize(input, out Grid p);

        //Assert
        result.Should().BeFalse();
    }



    [Test]
    public void TryParsePosition_ReturnTrueAndCorrectForValidPosition()
    {
        //Arrange
        string input = "5 5 S";
        var expectedResult = new Position(5,5, CardinalDirection.South);

        //Act
        var isSuccess = Input.TryParsePosition(input, out Position result);

        //Assert
        isSuccess.Should().BeTrue();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    [TestCase("-1 -1")]
    [TestCase("asdfghjkl")]
    [TestCase(" ")]
    [TestCase("-1 5")]
    [TestCase("5 -1")]

    public void TryParsePosition_ReturnFalseForInvalidPosition(string input)
    {
        //Arrange

        //Act
        var result = Input.TryParsePosition(input, out Position p);

        //Assert
        result.Should().BeFalse();
    }

    [Test]
    public void TryParseInstructions_ReturnCorrectForValidInstructions()
    {
        //Arrange
        string input = "LMRMM";
        Instruction[] expectedResult =
        {
            Instruction.TurnLeft,
            Instruction.Move,
            Instruction.TurnRight,
            Instruction.Move,
            Instruction.Move
        };
        

        //Act
        bool isSuccess = Input.TryParseInstructions(input, out Instruction[] result);

        //Assert
        isSuccess.Should().BeTrue();
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Test]
    [TestCase("asdfghjkl")]
    [TestCase("5 5 5")]
    [TestCase("5")]
    [TestCase("P")]
    [TestCase(" ")]
    [TestCase("MLMRMR R")]
    [TestCase("mlrmlm")]

    public void TryParseInstructions_ReturnFalseForInvalidInput(string input)
    {
        //Arrange

        //Act
        bool isSuccess = Input.TryParseInstructions(input, out Instruction[] i);

        //Assert
        isSuccess.Should().BeFalse();
    }
}
