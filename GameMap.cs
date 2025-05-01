using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class GameMap
    {
        //  2D grid, list representing the game map
        private List<List<char>> gameMap = new List<List<char>>();
        //  Dictionary storing rooms by their coordinates
        private Dictionary<(int x, int y), Room> roomMap = new Dictionary<(int x, int y), Room>();

        private static Random random = new Random();

        //  Players spawn coordinates
        private int spawnX;
        private int spawnY;

        //  Constructor sets spawn position
        public GameMap(int spawnX, int spawnY)
        {
            this.spawnX = spawnX;
            this.spawnY = spawnY;
        }

        //  Initializes the 2D game map by setting empty cells and a player
        public void CreateGameMap()
        {
            for (int i = 0; i < 9; i++)
            {
                List<char> row = new List<char>();
                for (int j = 0; j < 14; j++)
                {
                    row.Add('.');
                }
                gameMap.Add(row);
            }
            gameMap[spawnY][spawnX] = 'P';
        }
        //  Checks if player is outside the map bounds
        public bool checkPosition(int playerX, int playerY)
        {
            if (playerX < 0 || playerX > 13 || playerY < 0 || playerY > 8) // Define the border
            {
                return true;
            }
            return false;
        }

        //  Displays the current map
        public void DisplayMap()
        {
            foreach (var row in gameMap)
            {
                Console.WriteLine(string.Join("", row));
            }

        }
        //  Creates a new room with a random description and item
        private Room GenerateRoom(int x, int y)
        {
            string[] descriptions = {
                "You step into a dimly lit engine bay. \nSparks fly from torn cables, and a faint humming noise echoes through the walls.",
                "This looks like a maintenance room. \nTools are scattered on the floor, and blood stains mark the wall.",
                "A shattered control panel blinks weakly. \nSomeone tried to send a distress signal... and failed.",
                "The walls here pulse with organic growth. \nZerg creep has taken over the corridor floor.",
                "You hear distant chittering. \nA cocoon-like structure twitches in the corner.",
                "The corridor reeks of decay. \nAcid burns mark the bulkheads — Zergs were definitely here.",
                "You enter a dark supply room. \nSome crates are broken open, as if torn by claws.",
                "An abandoned armory. Most lockers are empty, \nbut a few glimmer with leftover gear.",
                "This medbay has been trashed. \nMedical beds are overturned, and strange green slime coats the walls.",
                "You walk into a silent hallway. The lights flicker, \nand your footsteps echo unnaturally loud.",
                "The air feels heavy here. \nSomething was watching you... or still is.",
                "A bloodied handprint smears the doorframe. \nNo bodies — just shadows."
            };

            Item items = Room.RandomItems[random.Next(Room.RandomItems.Length)];

            Random rand = new Random();
            string desc = descriptions[rand.Next(descriptions.Length)];

            Item item = Room.RandomItems[rand.Next(Room.RandomItems.Length)];

            return new Room(desc, item);
        }
        //  Retrieves a room at the given coordinates
        public Room GetRoom(int x, int y)
        {
            if(!roomMap.ContainsKey((x, y)))
            {
                roomMap[(x, y)] = GenerateRoom(x, y);
            }

            return roomMap[(x, y)];
        }
        //  Move the player in a given direction and updates the map
        public void MoveRight(ref int playerX, ref int playerY)
        {

            gameMap[playerY][playerX] = '.';
            playerX++;
            gameMap[playerY][playerX] = 'P';
        }

        public void MoveLeft(ref int playerX, ref int playerY)
        {

            gameMap[playerY][playerX] = '.';
            playerX--;
            gameMap[playerY][playerX] = 'P';

        }

        public void MoveUp(ref int playerX, ref int playerY)
        {
            gameMap[playerY][playerX] = '.';
            playerY--;           
            gameMap[playerY][playerX] = 'P';
        }
        
        public void MoveDown(ref int playerX, ref int playerY)
        {
            gameMap[playerY][playerX] = '.';
            playerY++;
            gameMap[playerY][playerX] = 'P';
        }
    }
}
