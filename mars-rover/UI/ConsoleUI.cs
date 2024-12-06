using mars_rover.Grids;
using mars_rover.RoverHandlers;

namespace mars_rover.UI
{
    public class ConsoleUI
    {
        //TO DO:
        //Implement UI interface to be able to switch to different UIs in the future

        private static ConsoleUI? instance;
        private ConsoleUI() { }
        public static ConsoleUI GetInstance()
        {
            instance ??= new ConsoleUI(); 
            return instance;    
        }

        public void PromptInput()
        {

        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DrawGrid(Grid grid, Rover rover)
        {
            char c = rover.CurrentPosition.Facing switch
            {
                CardinalDirection.North => '^',
                CardinalDirection.East => '>',
                CardinalDirection.South => 'v',
                CardinalDirection.West => '<',
                _ => ' '

            };
            for (int i = grid.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < grid.Width; j++)
                {
                    if (i == rover.CurrentPosition.Y && j == rover.CurrentPosition.X) Console.Write(c);
                    else Console.Write('_');
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
