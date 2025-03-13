using System;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {
        //  Objects for the player and rooms
        //public Player Player { get { return player; } }
        public Player player;
        private Room currentRoom;

        //  Game constructor, initializes the game contents, e.g. player and room
        public Game()
        {
            Console.WriteLine("Enter the player name: ");   // Ask the player for their name
            string playerName = Console.ReadLine(); // Create a variable for their name

            player = new Player(playerName,100);    // Create the player
            currentRoom = new Room("Damp, wet room");   // Create a room
        }
        // Main game loop method
        public void Start()
        {
            player.ResetInventory();    //  Remove inventory contents after testing methods

            bool playing = true;
            while (playing) // Iterate the game infinitely
            {
                Console.WriteLine("\n=-=-=-=-=-=-=-\nType 'help' for commands\n=-=-=-=-=-=-=-\nInput: ");   // Ask the player for their input
                string userInput = Console.ReadLine();

                switch (userInput)  // Switch cases for user input
                {
                    case "help":    // If the player enters 'help', they receive a list of commands which the game works on
                        Console.WriteLine("" +
                            "Type 'next room' in order to enter the next room" +
                            "\nType 'pick up' in order to pick up an item " +
                            "\nType 'inventory' in order to view your inventory " +
                            "\nType 'description' in order to view room description" +
                            "\nType 'status' in order to view player status");
                        break;

                    case "next room":   // If the player enters 'next room' the room and related items are generated
                        Console.WriteLine("Entered the next room"); // Inform the player that they've entered the room
                        currentRoom.RandomRoom();   // Method is called to generate a random room description
                        player.RandomItem();    // Method is called to generate a random item for the room
                        Console.WriteLine($"You found {player.GetItem()}"); // Inform the player about the item they've found
                        break;

                    case "pick up": // If the player enters 'pick up' the item is added to the players inventory
                        player.PickUpItem(player.GetItem());    // Methods are called which add the item to the players inventory
                        break;

                    case "inventory":   // If the player enters 'Inventory' the players inventory is displayed
                        Console.WriteLine($"Your inventory : {player.InventoryContents()}");    // Display the inventory calling the method which does so
                        break;

                    case "description": // If the player enters 'description' the rooms description is displayed, called by its relevant method
                        Console.WriteLine($"Current room description: '{currentRoom.GetDescription()}'");
                        break;

                    case "status":  // If the player enters 'status' their properties are displayed
                        Console.WriteLine($"Player status: \nPlayer - {player.Name} \nHealth - {player.Health}");
                        break;

                    default:    // If none of these inputs are matched, we assume that the player entered an invalid command, and inform them about this, no game logic changes
                        Console.WriteLine($"Input '{userInput}' not recognised, try again, or type help for a list of commands");
                        break;
                }
            }
        }
    }
}