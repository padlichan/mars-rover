using mars_rover.Grids;
using mars_rover.RoverHandlers;

namespace mars_rover.UI
{
    public static class ConsoleUI
    {
        //TO DO:
        //Implement UI interface to be able to switch to different UIs in the future

        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void DisplayMessageNoBreak(string message)
        {
            Console.Write(message);
        }

        public static void DrawGrid(Grid grid)
        {
            for (int i = grid.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < grid.Width; j++)
                {
                    if (grid.Map[j,i] != null) Console.Write(DirectionToSymbol(grid.Map[j,i].CurrentPosition.Facing));
                    else Console.Write('_');
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static string DirectionToSymbol(CardinalDirection direction) 
        {
            string symbol = direction switch
            {
                CardinalDirection.NORTH => "^",
                CardinalDirection.EAST => ">",
                CardinalDirection.SOUTH => "v",
                CardinalDirection.WEST => "<",
                _ => ""

            };
            return symbol;
        }
    }
}
