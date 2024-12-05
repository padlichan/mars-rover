namespace mars_rover;


internal class Program
{
    static void Main(string[] args)
    {
        //Prompt user to input plateau size
        Console.WriteLine("Welcome to Mars Rover!");
        string plateauInput;
        PlateauSize plateau;
        do
        {
            plateauInput = Input.GetInput("Enter plateau size (e.g. 5 5): ");

        }
        while (!Input.TryParsePlateauSize(plateauInput, out plateau));
        //Prompt user to input starting position
        string positionInput;
        Position position;
        do
        {
            positionInput = Input.GetInput("Enter starting position (e.g. 0 0 E):");
        }
        while(!Input.TryParsePosition(positionInput, out position));
        Console.WriteLine("Delivering rover...SUCCESS");
        //Prompt user for instructions
        string instructionsInput;
        Instruction[] instructions;

        do
        {
            instructionsInput = Input.GetInput("Enter rover instructions (e.g. LMMRMMR: ");
        }while(!Input.TryParseInstructions(instructionsInput, out instructions));
        Console.WriteLine("Performing instructions... SUCCESS");
    }
}
