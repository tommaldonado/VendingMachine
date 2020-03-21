using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public class Audit
    {

        public void AddToTheFile(string action, double amountAdded, double customerBalance)
        {
            string currentTime = DateTime.Now.ToString();

            string directory = Environment.CurrentDirectory;
            string fileName = "log.txt";

            string logPath = Path.Combine(directory, fileName);

            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                
                sw.WriteLine($"{currentTime} {action} {amountAdded:C} {customerBalance:C}");
                
            }

        }

        public void AddToTheFile(string itemName, string slotNumber, double itemPrice, double balance)
        {
            string currentTime = DateTime.Now.ToString();

            string directory = Environment.CurrentDirectory;
            string fileName = "log.txt";

            string logPath = Path.Combine(directory, fileName);

            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                
                sw.WriteLine($"{currentTime} {itemName} {slotNumber} {itemPrice:C} {balance:C} ");
                
            }
        }
        public void AddToTheFile(string action, double changeGiven)
        {
            string currentTime = DateTime.Now.ToString();

            string directory = Environment.CurrentDirectory;
            string fileName = "log.txt";

            string logPath = Path.Combine(directory, fileName);

            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
               
                sw.WriteLine($"{currentTime} {action} {changeGiven:C} $0.00");
            }
        }

    }
}
