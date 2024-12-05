using FluentAssertions;
using mars_rover;
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
        string result = Input.GetInput();

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
            $"Input Rover instructions: {Environment.NewLine}" +
            $"Input Rover instructions: {Environment.NewLine}" +
            $"Input Rover instructions: {Environment.NewLine}";

        var expectedResult = "M";


        // Act
        var originalOut = Console.Out;
        var originalIn = Console.In;
        var stringReader = new StringReader(simulatedInput);
        Console.SetIn(stringReader);

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        var result = Input.GetInput();

        // Assert
        result.Should().Be(expectedResult);
        expectedOutput.Should().Be(stringWriter.ToString());

        //Cleanup

        Console.SetOut(originalOut);
        Console.SetIn(originalIn);
    }
    [Test]
    public void ParseInput_ReturnCorrectForValidPosition()
    {
        //Arrange
        string input = "1 2 N";
        var expectedResult = new ParsedInput(null, new Position(1, 2, CardinalDirection.North), null);

        //Act
        bool isSuccess = Input.TryParseInput(input, out ParsedInput result);

        //Assert
        isSuccess.Should().BeTrue();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void ParseInput_ReturnCorrectForValidPlateauSize()
    {
        //Arrange
        string input = "5 5";
        var expectedResult = new ParsedInput(null, null, new PlateauSize(5, 5));

        //Act
        bool isSuccess = Input.TryParseInput(input, out ParsedInput result);

        //Assert
        isSuccess.Should().BeTrue();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void ParseInput_ReturnCorrectForValidInstructions()
    {
        //Arrange
        string input = "LMRMM";
        Instruction[] instructions =
        {
            Instruction.TurnLeft,
            Instruction.Move,
            Instruction.TurnRight,
            Instruction.Move,
            Instruction.Move
        };
        var expectedResult = new ParsedInput(instructions, null, null);

        //Act
        bool isSuccess = Input.TryParseInput(input, out ParsedInput result);

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
    [TestCase("5.2 5.4")]
    [TestCase("-2 -2")]
    [TestCase("-2 -2 E")]
    public void ParseInput_ReturnFalseAndNullForInvalidInput(string input)
    {
        //Arrange
        var expectedResult = new ParsedInput(null, null, null);

        //Act
        bool isSuccess = Input.TryParseInput(input, out ParsedInput result);

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
        isSuccess.Should().BeFalse();
    }
}
