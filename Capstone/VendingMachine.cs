using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Capstone
{
    public class VendingMachine
    {
        
        public const int startingItemCount = 5;
        public double SalesTotal { get; private set; }

        public Audit audit = new Audit();

        private static Dictionary<string, VendingMachineItem> itemInventory = new Dictionary<string, VendingMachineItem>();

        public void AddItemsToInventory(Dictionary<string, string[]> itemsDictionaryFromFile)
        {
            foreach (KeyValuePair<string, string[]> item in itemsDictionaryFromFile)
            {
                string slot = item.Key;
                string[] itemInfo = item.Value;
                string itemType = itemInfo[2];
                if (itemType == "Chip")
                {

                    itemInventory.Add(slot, new Chips(itemInfo));
                }
                else if (itemType == "Candy")
                {
                    itemInventory.Add(slot, new Candy(itemInfo));
                }
                else if (itemType == "Gum")
                {
                    itemInventory.Add(slot, new Gum(itemInfo));
                }
                else if(itemType == "Drink")
                {
                    itemInventory.Add(slot, new Drinks(itemInfo));
                }
            }
        }

        public void ReadyTheInventory()
        {
            Dictionary<string, string[]> itemsDictionaryFromFile = new Dictionary<string, string[]>();

            //read a file
            string fileDirectory = Environment.CurrentDirectory;
            string fileName = "VendingMachine.txt";
            string fullPathOfVendingFile = Path.Combine(fileDirectory, fileName);

            using (StreamReader sr = new StreamReader(fullPathOfVendingFile))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string slot = line.Substring(0, 2);
                    string info = line.Substring(3) + "|" + startingItemCount;
                    itemsDictionaryFromFile.Add(slot, info.Split('|'));
                }
            }
            //create/update inventory with items in the file
            AddItemsToInventory(itemsDictionaryFromFile);
        }

        public void DisplayItemList()
        {
            
            //take list created by AddItemsToInventory
            //if item is available, Get information about each item (slot #, name, purchase price)
            //if Count > 0, write slot, name, price
            Console.WriteLine("Items Available: ");
            Console.WriteLine();
            
            foreach(KeyValuePair<string, VendingMachineItem> item in itemInventory)
            {
                string slot = item.Key;
                VendingMachineItem vendItem = item.Value;

                if (vendItem.Count>0)
                {
                    
                    Console.WriteLine($"{slot, 5} | {vendItem.Name, -20} {vendItem.Price:C}");

                }
                else//if count !> 0, write "SOLD OUT"
                {
                    //if item is sold out, the item is display as "SOLD OUT"
                    Console.WriteLine($"{slot, 5} | **SOLD OUT**");
                } 
            }
            Console.WriteLine();
        }

        public double FeedMoney(double balance)
        {
            Console.Clear();
            bool isCorrect = false;
            while (!isCorrect)
            {
                
                Console.WriteLine($"Your balance is: {balance:C}");
                string action = "FEED MONEY";

                Console.WriteLine("How much money are you adding to your balance? (Please add whole dollar amounts)");
                try
                {
                    int amountAdded = int.Parse(Console.ReadLine());
                    if (amountAdded < 0)
                    {
                        break;
                    }
                    balance += amountAdded;
                    audit.AddToTheFile(action, amountAdded, balance);
                    isCorrect = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input try again");
                }
                catch(OverflowException ex)
                {
                    Console.WriteLine("This isn't a bank");
                }
                catch(Exception ee) 
                { 
                }
            }
            return balance;
        }

        public double SelectProduct(double customerBalance)
        {
            Console.Clear();
            //if customers balance is 0 and tries to select product
            if(customerBalance <= 0.0)
            {
                Console.WriteLine("You must deposit money before making a selection!");
                Console.ReadLine();
                return customerBalance;
            }
            
            //message: must deposit money first
            //return to purchase menu
            Console.WriteLine($"Your balance is: {customerBalance:C}");
            if (customerBalance > 0.75)
            {
                DisplayItemList();
            }
            

            bool isIncorrect = true;
            string customerInput = "";
            

            while (isIncorrect)
            {
                Console.WriteLine("Enter the slot number to purchase: ");
                customerInput = Console.ReadLine().ToUpper();
                

                if (!itemInventory.ContainsKey(customerInput))
                {
                    Console.WriteLine("That is an incorrect slot number");
                    isIncorrect = true;
                    
                }
                else
                {
                    isIncorrect = false;
                }
            }

            foreach (KeyValuePair<string, VendingMachineItem> item in itemInventory)
            {
                
                string slot = item.Key;
                VendingMachineItem vendItem = item.Value;
                


                if (customerInput == slot)
                {
                    if (customerBalance >= vendItem.Price && vendItem.Count > 0)
                    {

                        //customer balance is updated
                        customerBalance -= vendItem.Price;

                        //inventory is updated
                        vendItem.Count--;
                        vendItem.Sales++;

                        //balance of vending machine is updated
                        this.SalesTotal += vendItem.Price;

                        //item is dispensed
                        Console.WriteLine($"Here is your {vendItem.Name}");
                        //user receives a message
                        vendItem.MakeSound(vendItem.Type);
                        audit.AddToTheFile(vendItem.Name, slot, vendItem.Price, customerBalance);

                    }
                    else if (customerBalance < vendItem.Price)
                    {
                        Console.WriteLine("You don't have enough money for that item.");
                        Console.WriteLine("Press (esc) to leave and get more money or (Enter) to buy something else");
                        ConsoleKey keyInput = Console.ReadKey().Key;
                        if (keyInput == ConsoleKey.Escape)
                        {
                            
                            break;
                        }
                        else if (keyInput == ConsoleKey.Enter) 
                        {
                            
                            continue; 
                        }

                    }
                    else if (vendItem.Count == 0)
                    {
                        Console.WriteLine($"SOLD OUT");
                        
                    }
                }
                
            }

            //customer returns to purchase menu
            
            
            return customerBalance;

        }

        public void GenerateSalesReport()
        {
            Console.Clear();
            foreach(KeyValuePair<string, VendingMachineItem> item in itemInventory)
            {
                VendingMachineItem vi = item.Value;
                Console.WriteLine($"{vi.Name, -20} | {vi.Sales}");
                
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"**TOTAL SALES** {SalesTotal:C}");
            Console.WriteLine();
        }

        public void FinishTransaction(double customerBalance)
        {
            Console.Clear();
            string action = "GIVE CHANGE: ";
            Console.WriteLine("Here is your change: ");
            Change change = new Change(customerBalance);
            Console.WriteLine($"{change.Quarters} Quarter(s), {change.Dimes} Dime(s), {change.Nickels} Nickel(s)");
            audit.AddToTheFile(action, customerBalance);
            Console.WriteLine("Thank you!");
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadLine();

        }

    }
}
