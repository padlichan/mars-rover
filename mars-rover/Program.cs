namespace mars_rover;


internal class Program
{
    static void Main(string[] args)
    {
        //Prompt user to input plateau size
        //Prompt user to input starting position
        //Prompt user for instructions

        string input = Input.GetInput();
        Console.WriteLine(input);

        Input.TryParseInput(input, out ParsedInput parsedInput);
    }
}
