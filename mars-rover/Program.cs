namespace mars_rover;
using mars_rover.RoverHandlers;
using mars_rover.InputHandlers;


internal class Program
{
    static void Main(string[] args)
    {
        //Prompt user to input plateau size
        Console.WriteLine("Welcome to Mars Rover!");
        string plateauInput;
        Grid plateau;
        do
        {
            plateauInput = Input.GetInput("Enter plateau size (e.g. 5 5): ");
        }
        while (!Input.TryParsePlateauSize(plateauInput, out plateau));
        Console.WriteLine("Creating grid... SUCCESS");

        //Prompt user to input starting position
        string positionInput;
        Position position;
        do
        {
            positionInput = Input.GetInput("Enter starting position (e.g. 0 0 E):");
        }
        while (!Input.TryParsePosition(positionInput, out position));
        Console.WriteLine("Delivering rover to starting position...SUCCESS");
        Rover rover = new Rover(position, plateau);

        //Prompt user for instructions
        string instructionsInput;
        Instruction[] instructions;
        do
        {
            instructionsInput = Input.GetInput("Enter rover instructions (e.g. LMMRMMR: ");
        } 
        while (!Input.TryParseInstructions(instructionsInput, out instructions));
        if(rover.PerformInstructions(instructions))
        Console.WriteLine("Performing instructions... SUCCESS");
        else Console.WriteLine("Performing instructions... FAILED");
    }
}
