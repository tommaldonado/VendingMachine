using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Drinks : VendingMachineItem
    {
        public Drinks(string[] itemInfo) : base(itemInfo) { }



        public override void MakeSound(string type)
        {

            if (type == this.Type)
            {
                Console.WriteLine("Glug Glug, Yum!");
            }
        }

    }   
}
