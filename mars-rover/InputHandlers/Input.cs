namespace mars_rover.InputHandlers;
public static class Input
{
    public static string GetInput(string prompt)
    {
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
        }
        while (string.IsNullOrEmpty(input));

        return input;
    }

    public static bool TryParseInstructions(string input, out Instruction[] instructions)
    {
        instructions = new Instruction[0];
        string[] inputArray = input.Split(' ');
        if (inputArray.Length != 1) return false;
        string validCharacters = "MLR";
        if (!inputArray[0].All(x => validCharacters.Contains(x))) return false;
        List<Instruction> instructionList = new List<Instruction>();
        foreach (char c in inputArray[0]) 
        {
            instructionList.Add(charToInstruction(c));
        }
        instructions = instructionList.ToArray();
        return true;
    }

    private static Instruction charToInstruction(char input) 
    {
        Instruction instruction = input switch
        {
            'M' => Instruction.Move,
            'L' => Instruction.TurnLeft,
            'R' => Instruction.TurnRight,
            _ => Instruction.DoNothing,
        };

        return instruction;
    }

    public static bool TryParsePosition(string input, out Position position)
    {
        string[] inputArray = input.Split(' ');
        position = new Position(0,0,CardinalDirection.North);
        if (inputArray.Length != 3) return false;
        if (!int.TryParse(inputArray[0], out int x)) return false;
        if (!int.TryParse(inputArray[1], out int y)) return false;
        if (x < 0 || y < 0) return false;
        if (inputArray[2].Length!=1 || !TryParseCardinalDirection(inputArray[2][0], out CardinalDirection direction)) return false;

        position = new Position(x, y, direction);
        return true;
    }

    public static bool TryParsePlateauSize(string input, out Grid plateauSize)
    {
        string[] inputArray = input.Split(' ');
        plateauSize = new(0,0);
        if(inputArray.Length != 2) return false;
        if (!int.TryParse(inputArray[0], out int length)) return false;
        if (!int.TryParse(inputArray[1], out int width)) return false;
        if (length < 1 || width < 1) return false;
        plateauSize = new Grid(length, width);
        return true;
    }

    private static bool TryParseCardinalDirection(char input, out CardinalDirection direction)
    {
        direction = CardinalDirection.North;
        string validValues = "NESW";
        if (!validValues.Contains(input)) return false;
        direction = input switch
        {
            'N' => CardinalDirection.North,
            'W' => CardinalDirection.West,
            'S' => CardinalDirection.South,
            'E' => CardinalDirection.East,
            _ => CardinalDirection.North
        };
        return true;
    }

}
public enum Instruction
{
    TurnLeft,
    TurnRight,
    Move,
    DoNothing
}

public enum CardinalDirection
{
    North,
    East,
    South,
    West
}

public class Position(int x, int y, CardinalDirection direction)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public CardinalDirection Facing { get; set; } = direction;
}

public class Grid(int length, int width)
{
    public int Length { get; init; } = length;
    public int Width { get; init; } = width;
}
