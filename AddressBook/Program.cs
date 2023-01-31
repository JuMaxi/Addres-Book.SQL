using System;

namespace AddressBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ManagementContacts Access = new ManagementContacts();

            Menu CallMenu = new Menu();

            CallMenu.OptionsContacts(Access);
        }
    }
}
