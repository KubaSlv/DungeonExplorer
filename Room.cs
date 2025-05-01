using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Room
    {

        // Room description as private field
        private string description;
  
        private static Random random = new Random();

        // Private fields for items, logic and creatures

        private Item item;

        private bool itemExists = false;

        private bool itemPickedUp = false;

        private Monster monster;

        // Constructor for the room, sets its description, item and generates monster
        public Room(string description, Item item)
        {
            this.description = description;

            this.item = item;
            this.itemExists = item != null;
            this.monster = GenerateMonster();
        }

        // Method returning the description of the room
        public string GetDescription()
        {
            return description;
        }

        //  Returns the monster object
        public Monster GetMonster()
        {
            return monster;
        }
        //  Returns true if theres a monster and is alive
        public bool HasMonster()
        {
            return monster != null && monster.IsAlive();
        }
        //  Method to determine if a room will contain a monster
        private Monster GenerateMonster()
        {
            int chance = random.Next(0, 2);

            if (chance == 0) return null;
            else
            {
                string[] monsterNames = { "Zergling", "Roach" }; //  Create monsters
                int monsterIndex = random.Next(0, monsterNames.Length);
                if (monsterNames[monsterIndex] == "Zergling")
                {
                    return new Zergling("Zergling", 30);
                }
                else
                {
                    return new Roach("Roach", 55);
                }
            }
        }
        //  Assigns an item to the room
        public void SetItem(Item item)
        {
            this.item = item;
            this.itemExists = true;
        }
        //  Predefined pool of random items
        public static Item[] RandomItems =
        {
            new Weapon("Faulty Rifle", 10),
            new ObjectiveItem("Zerg Sample"),
            new Potion("Stim Pack", 20),
            new ObjectiveItem("Xel'naga fragment")           
        };
        //  Picks and returns a random item
        public Item RandomItem()
        {
            int index = random.Next(RandomItems.Length);
            item = RandomItems[index];
            itemExists = true;
            return item;
        }
        //  Returns if an item exists in the room
        public bool GetItemExists()
        {
            return itemExists;
        }
        //  Removes the item from the room
        public void RemoveItem()
        {
            item = null;
            itemExists = false;
        }
        //  Returns whether the item has been picked up
        public bool IsItemPickedUp()
        {
            return itemPickedUp;
        }
        //  Marks the item as picked up
        public void MarkItemPickedUp()
        {
            itemPickedUp = true;
        }

        //  Method for returning the item in the room

        public Item GetItem()
        {
            return item;
        }
    }
}