using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        // OLK - Well coded class, functions small and easy to understand.


        // Private fields for the players name, health, inventory and an item
        public string Name { get; private set; } // OLK - Good use of encapsulation
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        private string item;
        private bool itemExists = false; // Private bool for the existance of an item in a given room

        private static Random random = new Random();

        // Initialising instances of the player class with their name and health
        public Player(string name, int health) 
        {
            Name = name;
            Health = health;
        }

        // Method for randomising an item from an array for the player
        public string RandomItem()
        {
            string[] randomItems =
            {
                "Sword",
                "Gem fragement",
                "Healing potion"
            };

            int index = random.Next(randomItems.Length); // OLK - Nice use of random for room items.
            item = randomItems[index];
            itemExists = true;  // Boolean for declaring existance of the created, randomised item
            return item;
        }

        //  Method for returning the item
        public string GetItem()
        {
            return item;
        }

        //  Method for adding the item to the players inventory, also checking for the ability to add the item
        //  by checking for its existance
        public void PickUpItem(string item)
        {
            if (itemExists && item != null)     //  If the item exists
            {
                inventory.Add(item); // Add item to inventory
                Console.WriteLine($"Item '{item}' added to inventory"); //  Display to the player that the item has been successfully added  
                RemoveItem();   // Apply the appropriate logic to remove the item from existance, so it cannot be added again
            }
            else
            {
                Console.WriteLine("There is no item to pick up");   // If this item does not exist, nothing is added to the inventory and the player is informed
            }
        }

        //  Method for removing an item from existance
        private void RemoveItem()
        {
            item = null;    // Set the item to null, no item - no longer exists
            itemExists = false; // Set the bool to false, since the item no longer exists
        }
        // Method for returning the inventory contents.
        public string InventoryContents()
        {
            return string.Join(", ", inventory);    // Joins the array with ',' for when displaying the contents to the user
        }

        public void ResetInventory()
        {
            inventory.Clear();
            Console.WriteLine("Inventory reset.");
        }
    }
}