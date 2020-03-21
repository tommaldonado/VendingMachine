using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Candy : VendingMachineItem
    {
        public Candy(string[] itemInfo) : base(itemInfo) { }



        public override void MakeSound(string type)
        {
            
            if (type == this.Type)
            {
                Console.WriteLine("Munch Munch, Yum!");
            }   
        }
    }
}
