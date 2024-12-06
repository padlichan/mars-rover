using mars_rover.Grids;
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
        instructions = [];
        string[] inputArray = input.Split(' ');
        if (inputArray.Length != 1) return false;
        string validCharacters = "MLR";
        if (!inputArray[0].All(x => validCharacters.Contains(x))) return false;
        List<Instruction> instructionList = [];
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
        position = new Position(0, 0, CardinalDirection.North);
        if (inputArray.Length != 3) return false;
        if (!int.TryParse(inputArray[0], out int x)) return false;
        if (!int.TryParse(inputArray[1], out int y)) return false;
        if (x < 0 || y < 0) return false;
        if (inputArray[2].Length != 1 || !TryParseCardinalDirection(inputArray[2][0], out CardinalDirection direction)) return false;

        position = new Position(x, y, direction);
        return true;
    }

    public static bool TryParseGrid(string input, out Grid grid)
    {
        string[] inputArray = input.Split(' ');
        grid = new(0, 0);
        if (inputArray.Length != 2) return false;
        if (!int.TryParse(inputArray[0], out int length)) return false;
        if (!int.TryParse(inputArray[1], out int width)) return false;
        if (length < 1 || width < 1) return false;
        grid = new Grid(length, width);
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
