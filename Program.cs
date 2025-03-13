using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // OLK - good use of try catch block to prevent errors from crashing program
            try
            {
                Game game = new Game(); // Create a game object
                Testing testing = new Testing(game.player);
                testing.RunTests(); //  Run tests before running the game, to ensure everything works as intended

                game.Start();   // Runs it
            }
            catch(Exception ex) // Error check, if an exception is thrown, a message is displayed
            {
                Console.WriteLine($"An error occurred {ex.Message}");
            }
            finally // Once the game is finished running, we await for a user key input and end the program
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}
