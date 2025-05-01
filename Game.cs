using System;
using System.Collections.Generic;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;

namespace DungeonExplorer
{
    internal class Game
    {
        //  Objects for the player and rooms
        public Player player;
        private Room currentRoom;

        private Inventory inventory;
        private GameMap map;

        //  Player Coordinates
        private int playerX = 0, playerY = 0;

        //  Game constructor, initializes the game contents, e.g. player and room
        public Game()
        {
            inventory = new Inventory();
            //player = new Player(playerName,100, inventory);    // Create the player
            currentRoom = new Room("Cryo-pod open. Red lights flicker across the abandoned bridge. \nStatic hums in your helmet. The ship is dead silent — too silent.", null);   // Create a room
            map = new GameMap(playerX, playerY);
        }

        //  Update current Room after movement
        private void UpdateCurrentRoom()
        {
            currentRoom = map.GetRoom(playerX, playerY);
        }

        // Main game loop method
        public void Start()
        {

            inventory.ResetInventory();    //  Remove inventory contents after testing methods
            map.CreateGameMap();

            Console.Clear();

            bool playing = true;

            Console.WriteLine("> Initializing...");
            Thread.Sleep(2000);
            Console.WriteLine("> Vital signs stabilized.");
            Thread.Sleep(2000);
            Console.WriteLine("> Cryo - sleep suspended.");
            Thread.Sleep(2000);
            Console.WriteLine("> Marine, identify yourself for mission logs:");   // Ask the player for their name
            string playerName = Console.ReadLine(); // Create a variable for their name
            player = new Player(playerName, 100, inventory);    // Create the player

            while (playing) // Iterate the game infinitely
            {
                map.DisplayMap();
                Console.WriteLine("\n=-=-=-=-=-=-=-\nType 'help' for commands\n=-=-=-=-=-=-=-\nInput: ");   // Ask the player for their input
                string userInput = Console.ReadLine().ToLower();

                switch (userInput)  // Switch cases for user input
                {
                    case "help":    // If the player enters 'help', they receive a list of commands which the game works on
                        Console.WriteLine("" +
                            "Type 'right', 'left', 'up' or 'down' for movement" +
                            "\nType 'pick up' in order to pick up an item " +
                            "\nType 'inventory' in order to view your inventory " +
                            "\nType 'description' in order to view room description" +
                            "\nType 'status' in order to view player status" +
                            "\nType 'use item' in use your items" +
                            "\nType 'exit' to exit game");
                        break;
                        // Cases for movement
                    case "up":
                        playerY--;
                        if (map.checkPosition(playerX, playerY))    //  Check if the player is moving within bounds
                        {
                            playerY++;
                            Console.WriteLine("Out of bounds\n");
                        }
                        else // If they are, prepare the room
                        {
                            playerY++;
                            map.MoveUp(ref playerX, ref playerY);
                            UpdateCurrentRoom();
                            Room currentRoom = map.GetRoom(playerX, playerY);
                            CheckForMonster();
                        }                        
                        break;
                        
                    case "down":
                        playerY++;
                        if (map.checkPosition(playerX, playerY))
                        {
                            playerY--;
                            Console.WriteLine("Out of bounds\n");   //  error checking
                        }
                        else
                        {
                            playerY--;
                            map.MoveDown(ref playerX, ref playerY);
                            UpdateCurrentRoom();
                            Room currentRoom = map.GetRoom(playerX, playerY);
                            CheckForMonster();
                        }
                        break;

                    case "left":
                        playerX--;
                        if(map.checkPosition(playerX, playerY))
                        {
                            playerX++;
                            Console.WriteLine("It's the edge of the ship, you cannot go any further\n");
                        }
                        else
                        {
                            playerX++;
                            map.MoveLeft(ref playerX, ref playerY);
                            UpdateCurrentRoom();
                            Room currentRoom = map.GetRoom(playerX, playerY);
                            CheckForMonster();
                        }

                        break;

                    case "right":
                        playerX++;
                        if(map.checkPosition(playerX, playerY))
                        {
                            playerX--;
                            Console.WriteLine("Out of bounds\n");
                        }
                        
                        else
                        {
                            playerX--;
                            map.MoveRight(ref playerX, ref playerY);
                            UpdateCurrentRoom();
                            Room currentRoom = map.GetRoom(playerX, playerY);
                            CheckForMonster();
                        }
                        break;
                        //  Case for picking up items
                    case "pick up":
                        if (currentRoom.IsItemPickedUp())
                        {
                            Console.WriteLine("You already picked up the item from this room.");
                        }
                        else if (currentRoom.GetItemExists())
                        {
                            player.PickUpItem(currentRoom);
                            currentRoom.MarkItemPickedUp();
                        }
                        else
                        {
                            Console.WriteLine("There is no item in this room.");
                        }
                        break;

                    case "inventory":   // If the player enters 'Inventory' the players inventory is displayed
                        Console.WriteLine($"Your inventory : {inventory.InventoryContents()}");    // Display the inventory calling the method which does so
                        Console.WriteLine("Enter 'discard' to discard an item or 'close' to close");
                        string userItemInput = Console.ReadLine();
                        if(userItemInput == "close")
                        {
                            break;
                        }
                        else if(userItemInput == "discard")
                        {
                            player.DropItemFromInventory();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input");
                        }
                        
                        break;

                    case "description": // If the player enters 'description' the rooms description is displayed, called by its relevant method
                        Console.WriteLine($"Current room description: '{currentRoom.GetDescription()}'");
                        break;
                        //  Case for the player to use items (used only outside of fights)
                    case "use item":
                        Console.WriteLine($"Your inventory : {inventory.InventoryContents()}");
                        Console.WriteLine("Enter item name:");
                        string itemName = Console.ReadLine();
                        player.UseItem(null, itemName);
                        break;

                    case "status":  // If the player enters 'status' their properties are displayed
                        player.Stats();
                        break;

                    case "exit":
                        Environment.Exit(0);
                        break;

                    default:    // If none of these inputs are matched, we assume that the player entered an invalid command, and inform them about this, no game logic changes
                        Console.WriteLine($"Input '{userInput}' not recognised, try again, or type help for a list of commands");
                        break;
                }
            }
        }
        //  Checks if the room contains a monster and starts combat if it does
        private void CheckForMonster()
        {
            if (currentRoom.HasMonster())
            {
                Console.WriteLine($"There is a {currentRoom.GetMonster().Name} here...");
                player.StartCombat(currentRoom.GetMonster());
            }
            else
            {
                Console.WriteLine("The room is empty. No Zerg here.");
            }

            // Check for item in the room
            if (!currentRoom.IsItemPickedUp() && currentRoom.GetItemExists())
            {
                Console.WriteLine($"You see a {currentRoom.GetItem().GetItemName()} lying on the floor.");
            }
        }
    }
}
