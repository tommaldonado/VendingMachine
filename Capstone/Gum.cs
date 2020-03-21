using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Gum : VendingMachineItem
    {
        public Gum(string[] itemInfo) : base(itemInfo) { }


        public override void MakeSound(string type)
        {

            if (type == this.Type)
            {
                Console.WriteLine("Chew Chew, Yum!");
            }
        }
       






    }
}
