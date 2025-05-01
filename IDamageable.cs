using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    //  Interface for things that can take damage
    public interface IDamageable
    {
        //  Reduces health by given amount
        void TakeDamage(int amount);
        //  Hleath property to track current hp
        int Health { get; set; }
    }
}
