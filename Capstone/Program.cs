using System;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            //A way to update vending machine
            //file that cotains list of items in correct format
            //create/update inventory with items in the file
            //if item is in file, When vending machine starts, slot indicated is filled with five of that item
            //constant fillWithFive
            VendingMachineCLI commandLineProgram = new VendingMachineCLI();

            commandLineProgram.StartTheMachine();


        }
    }
}
