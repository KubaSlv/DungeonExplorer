using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    //  Abstract base class for all items that can be collected in the game
    public abstract class Item : ICollectible
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
        public virtual bool IsConsumable => false;

        public Item(string name, string type, int value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
        //  Called when the item is picked up by the player
        public void Collect()
        {
            Console.WriteLine($"{Name} has been collected");
        }
        //  Return the item's name
        public string GetItemName()
        {
            return Name;
        }
        //  Virtual method to use the item
        public virtual void Use(Player player, Monster monster)
        {
            Console.WriteLine($"{Name} has no use effect.");
        }

    }    
    //  Represents a healing item that restores health as 'potion'
    public class Potion : Item
    {
        public int HealAmount { get; set; }

        public override bool IsConsumable => true;  //  Consumable object
        public Potion(string name, int healAmount) : base(name, "potion", healAmount)
        {
            HealAmount = healAmount;
        }
        //  Heals the player by a certain amount
        public override void Use(Player player, Monster monster)
        {
            player.Health += HealAmount;
            Console.WriteLine($"{player.Name} used {Name} and healed for {HealAmount} HP!");
        }
    }
    //  Represents a weapon that can be used in or outside combat
    public class Weapon : Item
    {
        public int Damage { get; set; }

        public override bool IsConsumable => true;  //  Consumable object
        public Weapon(string name, int damage) : base(name, "weapon", damage)
        {
            Damage = damage;
        }

        public override void Use(Player player, Monster monster)
        {
            Console.WriteLine($"You armed yourself with a {Name}. Gants +{Damage} bonus damage." +
                $"\nIt probably won't last more than one fight");

            player.AddAttackBonus(Damage);
        }
    }
    //  Objective item - no real use
    public class ObjectiveItem : Item
    {
        public ObjectiveItem(string name) : base(name, "objective", 0) 
        {

        }

        public override void Use(Player player, Monster monster)
        {
            Console.WriteLine($"You try to use {Name}. \nYou don't know what to do with it. \nBut it seems important...");

        }
    }
}
