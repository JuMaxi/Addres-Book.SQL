using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class Menu
    {
        public void WriteNameCompany()
        {
            Console.WriteLine("|-----------------------|");
            Console.WriteLine("|   Happy Address Book  |");
            Console.WriteLine("|-----------------------|");
            Console.WriteLine(" ");
        }
        public void WriteText()
        {
            WriteNameCompany();

            Console.WriteLine("Please choose the Option you want: ");
            Console.WriteLine("1) Add new contact:");
            Console.WriteLine("2) Show contacts list:");
            Console.WriteLine("3) Remove contact:");
            Console.WriteLine("4) Update contact:");
            Console.WriteLine("5) Exit: ");
            Console.Write("-->");
        }
        public void ExitMessage()
        {
            Console.WriteLine(" ");
            Console.Write("Type any key to return");
            Console.ReadKey();
            Console.Clear();
        }

        public List<string> ReturnUpdates(List<string> Updates)
        {
            WriteNameCompany();

            Console.Write("Please type the ID:");
            Updates.Add(Console.ReadLine());


            Console.WriteLine("Please, choose the table you want to make Update: ");
            Console.WriteLine("1) Contacts: ");
            Console.WriteLine("2) Phones: ");
            Console.Write("--> ");
            string Table = Console.ReadLine();

            Console.WriteLine(" ");

            if (Table == "1")
            {
                Updates.Add("Contact");
                Console.WriteLine("Please, choose the field you want to make Update:");
                Console.WriteLine("1) Name: ");
                Console.WriteLine("2) Address: ");
                Console.WriteLine("3) Email: ");
                Console.Write("--> ");
                string Field = Console.ReadLine();

                if (Field == "1")
                {
                    Updates.Add("Name");
                }
                else
                {
                    if (Field == "2")
                    {
                        Updates.Add("Address");
                    }
                    else
                    {
                        Updates.Add("Email");
                    }
                }
                Console.WriteLine(" ");

                Console.WriteLine("Please, type the new information (the change you want): ");
                Console.Write("--> ");
                Updates.Add(Console.ReadLine());

                Console.WriteLine(" ");

                Console.WriteLine("This field was changed successful");
            }
            if (Table == "2")
            {
                Updates.Add("Phones");

                Console.WriteLine("Please, type the field of you want to change: ");
                Console.WriteLine("1) Type of Phone");
                Console.WriteLine("2) Phone Number:");
                Console.Write("--> ");
                string Phones = Console.ReadLine();
                if (Phones == "1")
                {
                    Updates.Add("Kind");
                }
                else
                {
                    Updates.Add("Phone");
                }

                Console.WriteLine(" ");

                Console.WriteLine("Please, type the new information (the change you want): ");
                Console.Write("--> ");
                Updates.Add(Console.ReadLine());

                Console.WriteLine(" ");

                Console.WriteLine("This field was changed successful");
            }
            return Updates;
        }


        public void OptionsContacts(ManagementContacts AccessMC)
        {
            string Choose = "0";
            List<Phones> OptionsPhone = new List<Phones>();

            while (Choose != "5")
            {
                WriteText();

                Choose = Console.ReadLine();

                Console.Clear();

                if (Choose == "1")
                {
                    WriteNameCompany();

                    Console.Write("Please type the Name + Last Name: ");
                    string Name = (Console.ReadLine());

                    Console.Write("Please type the Address: ");
                    string Address = Console.ReadLine();

                    Console.Write("Please type the Email: ");
                    string Email = Console.ReadLine();
                    Console.Clear();

                    PhoneMenu PhoneMenu = new PhoneMenu();

                    OptionsPhone = PhoneMenu.ReadOptionsPhone();

                    try
                    {
                        Contacts AccessContacts = new Contacts(Name, Address, Email, OptionsPhone);
                        AccessMC.AddNewContact(AccessContacts);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Something is wrong. Check the error message: " + ex.Message);
                    }

                    Console.Clear();
                }
                if (Choose == "2")
                {
                    AccessMC.ShowContacts();
                    ExitMessage();
                }
                if (Choose == "3")
                {
                    WriteNameCompany();
                    Console.Write("Please, type the ID: ");
                    string ID = Console.ReadLine();
                    int IDInt = Convert.ToInt32(ID);

                    AccessMC.RemoveContacts(IDInt);
                    ExitMessage();
                }
                if (Choose == "4")
                {
                    List<string> Updates = new List<string>();

                    Updates = ReturnUpdates(Updates);

                    int IDInt = Convert.ToInt32(Updates[0]);

                    AccessMC.UpdateContacts(IDInt, Updates[1], Updates[2], Updates[3]);
                    ExitMessage();
                }
            }
        }
    }
}
