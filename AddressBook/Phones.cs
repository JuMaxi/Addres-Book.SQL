using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class Phones
    {
        public string Type; // 1 = Mobile, 2 = Home, 3 = Business
        public string Number;

        public Phones (string type, string number)
        {
            Type = type;
            Number = number;
        }
    }
}
