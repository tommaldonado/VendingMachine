using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public abstract class VendingMachineItem
    {
        //public string Slot { get; }
        public string Name { get; }
        public double Price { get; }
        //Find a way to get the Type to alter the subclass
        public string Type { get; }
        public int Count { get; set; }
        public int Sales { get; set; }

        public VendingMachineItem(string[]itemInfo)
        {
            
            this.Name = itemInfo[0];
            this.Price = double.Parse(itemInfo[1]);
            this.Type = itemInfo[2];
            this.Count = int.Parse(itemInfo[3]);
        }

        public abstract void MakeSound(string type);
        
        
            
    }
}
