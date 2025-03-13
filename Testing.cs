using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Testing
    {
        private Player player;
        //Game game = new Game();

        public Testing(Player player)
        {
            this.player = player;  // Use public property to get access to player
        }

        public void RunTests()  //  Method for running all of the tests
        {
            TestInventoryStartEmpty();
            TestPickUpItem();
            TestInventoryAfterItemPickUp();
        }

        private void TestInventoryStartEmpty()  // Test method to check the Inventory is empty at the start
        {
            Debug.Assert(string.IsNullOrEmpty(player.InventoryContents()), "Inventory should be empty at start");   //  Ensure Inventory is empty, if not inform the user
            Console.WriteLine("Test for inventory beginning passed");   // Test successful
        }

        private void TestPickUpItem()   // Test method to ensure picking up items works
        {
            string item = player.RandomItem();  //  Simulate picking up an item to test if it works
            player.PickUpItem(item);
            Debug.Assert(player.InventoryContents().Contains(item), "Inventory should contain the added item");
            Console.WriteLine("Test for picking up items passed");
        }

        private void TestInventoryAfterItemPickUp() // Test method to see if the Inventory is in the correct state, once an item has been added
        {
            string item = player.RandomItem();  //  Simulate adding items to the inventory to test if it works
            player.PickUpItem(item);
            string inventory = player.InventoryContents();
            Debug.Assert(inventory.Contains(item), $"Inventory does not contain '{item}' After it being added");
            Console.WriteLine("Test for adding items to inventory passed");
        }
    }
}
