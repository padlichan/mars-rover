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

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DrawGrid(Grid grid)
        {
            for (int i = grid.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < grid.Width; j++)
                {
                    if (grid.Map[j,i] != null) Console.Write(directionToSymbol(grid.Map[j,i].CurrentPosition.Facing));
                    else Console.Write('_');
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public string directionToSymbol(CardinalDirection direction) 
        {
            string symbol = direction switch
            {
                CardinalDirection.North => "^",
                CardinalDirection.East => ">",
                CardinalDirection.South => "v",
                CardinalDirection.West => "<",
                _ => ""

            };
            return symbol;
        }
    }
}
