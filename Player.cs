using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player : Creature, IDamageable
    {
        // OLK - Well coded class, functions small and easy to understand.

        private Inventory inventory;

        private int attackBonus = 0;

        // Constructor initialising instances of the player class with their name and health and inventory
        public Player(string name, int health, Inventory inventory) : base(name, health)
        {
            Name = name;
            Health = health;
            this.inventory = inventory; 
            
        }
        //  Handles the fight between player and the enemy
        public void StartCombat(Monster monster)
        {
            while (monster.IsAlive() && this.IsAlive())
            {
                Console.WriteLine($"\nYour HP: {this.Health} | Monster HP: {monster.Health}");
                Console.WriteLine("Prepare your 'attack':");


                string choice = Console.ReadLine();

                if (choice == "attack")
                {
                    Console.WriteLine("You attacked!");
                    int damage = this.Attack();
                    monster.TakeDamage(damage);
                    Console.WriteLine($"You dealt {damage} damage!");
                }

                //  If the enemy (monster) is alive, they attack back
                if (monster.IsAlive())
                {
                    int monsterDamage = monster.Attack();
                    this.TakeDamage(monsterDamage);
                    Console.WriteLine($"{monster.Name} dealt {monsterDamage} damage!");
                }
            }
            //  When the fight ends, check who wins / who's alive
            if (this.IsAlive())
            {
                Console.WriteLine($"The {monster.Name} screeches and collapses in a heap of gore.. \nEnemy neutralized.");
                ResetAttackBonus();
            }
            else
            {
                Console.WriteLine("Your visor cracks... static fills your comms... \nThe last thing you hear is the screech of the swarm.");
                Console.WriteLine("You Died\nPress any key to continue");
                Console.ReadKey();
                Environment.Exit(0);    //  End the game
                Console.WriteLine("Exited.");   //  Small detail, sometimes lines of code leak after the game is exited, so to keep immersion, I added this.
            }
        }
        //  Method for handling using items (outside of combat)
        public void UseItem(Monster monster, string itemName)
        {            
            Item item = inventory.GetItem(itemName);

            if (item == null)
            {
                Console.WriteLine("You don't have that item!");
                return;
            }

            item.Use(this, monster);
            //  If the item should be only used once (e.g. a potion) it will be removed after use
            if (item.IsConsumable)  
            {
                inventory.RemoveItem(item);
            }
        }
        //  Picks up item if it exists in given room
        public void PickUpItem(Room room)
        {
            if (room != null && room.GetItemExists())
            {
                Item roomItem = room.GetItem();
                if (roomItem is ICollectible collectible)
                {
                    collectible.Collect();
                }

                inventory.AddItemToInventory(roomItem);
                Console.WriteLine($"You put '{roomItem.Name}' to your Inventory");
                room.RemoveItem();
            }
            else
            {
                Console.WriteLine("There is no item to pick up");   //  error checking
            }
        }

        public void DropItemFromInventory()
        {
            Console.WriteLine("Which item would you like to drop?");
            Console.WriteLine("Inventory: " + inventory.InventoryContents());
            Console.Write("Enter item name: ");
            string input = Console.ReadLine();

            Item itemToDrop = inventory.GetItem(input);
            if (itemToDrop != null)
            {
                inventory.RemoveItem(itemToDrop);
                Console.WriteLine($"You dropped {itemToDrop.Name}.");
            }
            else
            {
                Console.WriteLine("Item not found in your inventory.");
            }
        }
        //  Attacks with random base + bonus
        public int Attack()
        {
            Random rng = new Random();
            int baseDamage = rng.Next(10, 20);
            int totalDamage = baseDamage + attackBonus;

            Console.WriteLine($"You attack the monster for {totalDamage} damage! (Base: {baseDamage}, Bonus: {attackBonus})");
            return totalDamage;
        }
        //  Method for increasing damage
        public void AddAttackBonus(int bonus)
        {
            attackBonus += bonus;
            Console.WriteLine($"Attack increased by {bonus}");
        }
        //  Handles taking damage, health can't go below 0
        public override void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
                Health = 0;
            Console.WriteLine($"{Name} takes {damage} damage. Health is now {Health}.");
        }
        //  Resets bonus damage to 0 (removes bonus)
        public void ResetAttackBonus()
        {
            attackBonus = 0;
        }
    }
}