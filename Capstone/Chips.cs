using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Chips : VendingMachineItem
    {
        public Chips(string[]itemInfo) : base(itemInfo) {}
       
        

        public override void MakeSound(string type)
        {
            
            Console.WriteLine("Crunch Crunch, Yum!");
        }

        

        
        

    }
}
