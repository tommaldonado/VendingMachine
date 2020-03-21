using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{            //When vending machine starts, slot indicated is filled with five of that item

    public class VendingMachineCLI
    {

        

        VendingMachine vendMach = new VendingMachine();
        public void StartTheMachine()
        {
            
            vendMach.ReadyTheInventory();

            string userChoice = "";
            while (userChoice != "3")
            {
                
                Console.WriteLine("Press 1 to display a list of available items");
                Console.WriteLine("Press 2 to purchase an item");
                Console.WriteLine("Press 3 to exit");
                userChoice = Console.ReadLine();

                if (userChoice == "1")
                {
                    Console.Clear();
                    vendMach.DisplayItemList();
                }
                else if (userChoice == "2")
                {
                    DisplayPurchaseMenu();
                }
                else if (userChoice == "4")
                {
                    vendMach.GenerateSalesReport();
                }
            }
        }

        public void DisplayPurchaseMenu()
        {
            string userChoice = "";
            double customerBalance = 0.0;
            while (userChoice != "3") //Finish gets you out of this menu
            {
                
                Console.WriteLine($"Your current balance is: {customerBalance:C}");
                Console.WriteLine();
                Console.WriteLine("Enter 1 to add to your balance");
                Console.WriteLine("Enter 2 to select a product to purchase");
                Console.WriteLine("Enter 3 to finish the transaction");
                userChoice = Console.ReadLine();

                if (userChoice == "1")
                {
                    customerBalance = vendMach.FeedMoney(customerBalance);
                    Console.Clear();
                    
                }
                else if (userChoice == "2")
                {
                    customerBalance = vendMach.SelectProduct(customerBalance);

                }

            }
            
            vendMach.FinishTransaction(customerBalance);
            Console.Clear();
        }

        

        
    }
}

