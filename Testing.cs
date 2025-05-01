using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Testing
    {
        //  Dependencies for testing
        private Player player;
        private Room currentRoom;
        private Inventory inventory;

        private string logFilePath = "test_log.txt";

        //  Constructor prepares testing environment 
        public Testing(Player player, Room currentRoom, Inventory inventory)
        {
            //  Access to objects
            this.player = player;  
            this.currentRoom = currentRoom; 
            this.inventory = inventory;
        }

        public void RunTests()  //  Method for running all of the tests
        {
            Console.WriteLine("=-=-Testing process has begun=-=-");
            LogToFile("=-=-Testing process has begun=-=-");

            TestInventoryStartEmpty();
            TestPickUpItem();
            TestInventoryAfterItemPickUp();

            Console.WriteLine("=-=-Testing process has ended=-=-");
            LogToFile("=-=-Testing process has ended=-=-");

            string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
            string logFilePath = Path.Combine(logDirectory, "test_log.txt");

            //VVVV Below I've commented debugging help to help locate the file paths, especially the txt file

            //Console.WriteLine($"Log Directory- {logDirectory} \nlogFilePath- {logFilePath}");

            Console.WriteLine("Tests successfully completed- \npress any key to continue:");
            Console.ReadKey();

        }

        private void TestInventoryStartEmpty()  // Test method to check the Inventory is empty at the start
        {
            Debug.Assert(string.IsNullOrEmpty(inventory.InventoryContents()), "Inventory should be empty at start");   //  Ensure Inventory is empty, if not inform the user
            Console.WriteLine("Test for inventory beginning passed");   // Test successful
            LogToFile("Test for inventory beginning passed");
        }

        private void TestPickUpItem()   // Test method to ensure picking up items works
        {
            Item item = currentRoom.RandomItem();  //  Simulate picking up an item to test if it works
            player.PickUpItem(currentRoom);
            Debug.Assert(inventory.InventoryContents().Contains(item.Name), "Inventory should contain the added item");
            Console.WriteLine("Test for picking up items passed");
            LogToFile("Test for picking up items passed");
        }

        private void TestInventoryAfterItemPickUp() // Test method to see if the Inventory is in the correct state, once an item has been added
        {
            Item item = currentRoom.RandomItem();  //  Simulate adding items to the inventory to test if it works
            player.PickUpItem(currentRoom);
            string inventoryDebug = inventory.InventoryContents();
            Debug.Assert(inventoryDebug.Contains(item.Name), $"Inventory does not contain '{item.Name}' after it being added");
            Console.WriteLine("Test for adding items to inventory passed");
            LogToFile("Test for adding items to inventory passed");
        }

        private void LogToFile(string message)
        {
            try
            {
                // Append the message to the log file
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
