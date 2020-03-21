using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Change
    {
        public int Quarters { get; private set; }
        public int Dimes { get; private set; }
        public int Nickels { get; private set; }

        public Change (double balance)
        {
            Quarters = (int)(balance / .25);
            balance %= .25d;
            
            Dimes = (int)(balance / .10);
            balance %= .10d;
            
            Nickels = (int)(balance / .05);
            balance %= .05d;

        }

    }
}
