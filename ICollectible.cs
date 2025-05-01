using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    //  Interface for items that can be picked up by the player
    public interface ICollectible
    {
        //  Collect the item (shows a message when it's collected)
        void Collect();
        //  Get the name of the item
        string GetItemName();
    }
}
