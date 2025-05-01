using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    //  Base class for all characters
    public class Creature : IDamageable
    {
        public string Name { get; set; }
        public int Health { get; set; }

        public Creature(string name, int health)
        {
            Name = name;
            Health = health;
        }
        //  Reduces health by the damage amount
        public virtual void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }
        //  Returns true if creature is still alive
        public bool IsAlive()
        {
            return Health > 0;
        }
        //  Displays creature's stats to the console
        public void Stats()
        {
            Console.WriteLine($"Name - {Name}\nHealth - {Health}");
        }
    }
    //  Represents a monster (enemy) inherited from Creature base
    public class Monster : Creature
    {
        public Monster(string name, int health) : base(name, health)
        {

        }
        //  Base attack logic
        public virtual int Attack()
        {
            Random rng = new Random();
            int damage = rng.Next(5, 15);
            Console.WriteLine($"{Name} attacks for {damage} damage!");
            return damage;
        }

    }
    //  Specific Monsters
    public class Zergling : Monster
    {
        public Zergling(string name, int health) : base(name, health) { }

        public override int Attack()
        {
            Random rng = new Random();
            int damage = rng.Next(3, 10);
            Console.WriteLine($"{Name} Bites you for '{damage}' damage!");
            return damage;
        }
    }

    public class Roach : Monster
    {
        public Roach(string name, int health) : base(name, health) { }

        public override int Attack()
        {
            Random rng = new Random();
            int damage = rng.Next(10, 20);
            Console.WriteLine($"{Name} Spits acid for {damage} damage!");
            return damage;
        }
    }
}
