using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Inventory
    {
        //  Internal list that stores all items the player holds
        private List<Item> inventory = new List<Item>();

        //  Adds an item to the inventory
        public void AddItemToInventory(Item item)
        {
            inventory.Add(item);
        }

        //  Returns a string showing all item names, sorted alphabetically
        public string InventoryContents()
        {

            return string.Join(", ", inventory
        .OrderBy(i => i.Name)
        .Select(i => i.Name));
        }
        //  Remove a specific item from the inventory
        public void RemoveItem(Item item)
        {
            inventory.Remove(item);
        }
        //  return item by name or null if not found
        public Item GetItem(string itemName)
        {
            return inventory.FirstOrDefault(i => i.Name.ToLower() == itemName.ToLower());
        }
        //  Clear all items from the inventory
        public void ResetInventory()
        {
            inventory.Clear();
            Console.WriteLine("Inventory reset.");
        }
    }
}
