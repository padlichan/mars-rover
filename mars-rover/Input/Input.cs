namespace mars_rover;

public static class Input
{
    public static string GetInput()
    {
        string? input = "";
        do
        {
            Console.Write("Input Rover instructions: ");
            input = Console.ReadLine();
            Console.WriteLine();
        }
        while (string.IsNullOrEmpty(input));

        return input;
    }

    public static bool ParseInput(string input)
    {

        return false;
    }

}

public enum Instruction
{
    TurnLeft,
    TurnRight,
    Move
}

public enum CardinalDirection
{
    North,
    East,
    South,
    West
}

public class Position
{
    public int X { get; set; }
    public int y { get; set; }
    public CardinalDirection Facing { get; set; }
}

public class PlateauSize
{
    public int Length { get; init; }
    public int Width { get; init; }
}
