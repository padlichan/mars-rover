using FluentAssertions;
using mars_rover;
using System.ComponentModel.DataAnnotations;
namespace mars_rover_test;

public class InputTest
{
    [SetUp]
    public void Setup()
    {
    }

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
}
