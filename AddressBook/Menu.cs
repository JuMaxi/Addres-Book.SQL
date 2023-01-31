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
        public int CalculateID(ManagementContacts Access)
        {
            int LastID = 0;

            if (Access.AddContacts.Count == 0)
            {
                LastID = (Access.AddContacts.Count) + 1;
            }
            else
            {
                LastID = (Access.AddContacts[Access.AddContacts.Count - 1].ID) + 1;
            }

            return LastID;
        }

        public void ReadFile(ManagementContacts AccessMC)
        {
            string Path = @"C:\Dev\AddressBook\AddressBook\database.csv";

            if (File.Exists(Path))
            {
                string[] ReadTxt = File.ReadAllLines(Path);

                foreach (string Line in ReadTxt)
                {
                    string[] BreakTxt = Line.Split(";");
                    int NumberID = Convert.ToInt32(BreakTxt[0]);
                    List<Phones> OptionsPhone = new List<Phones>();

                    for (int Position = 4; Position < BreakTxt.Length - 1; Position = Position + 2)
                    {
                        Phones NewPhone = new Phones(BreakTxt[Position], BreakTxt[Position + 1]);
                        OptionsPhone.Add(NewPhone);
                    }

                    Contacts AccessContacts = new Contacts(NumberID, BreakTxt[1], BreakTxt[2], BreakTxt[3], OptionsPhone);

                    AccessMC.AddNewContact(AccessContacts);
                }
            }
        }

        public void WriteFile(List<Contacts> WriteContact)
        {
            string Path = @"C:\Dev\AddressBook\AddressBook\database.csv";
            List<string> WriteFile = new List<string>();

            foreach (Contacts Line in WriteContact)
            {
                string LineActual = (Line.ID + ";" + Line.Name + ";" + Line.Address + ";" + Line.Email.EmailAddress);
                string LinePhones = "";
                string LineActualPhones = "";

                for (int Position = 0; Position < Line.Phones.Count; Position++)
                {
                    LineActualPhones = (";" + (Line.Phones[Position].Type) + ";" + (Line.Phones[Position].Number));
                    LinePhones = LinePhones + LineActualPhones;

                }
                WriteFile.Add(LineActual + LinePhones);
            }
            File.WriteAllLines(Path, WriteFile);
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

        bool ExitContacts = false;
        string Choose = "0";

        public void OptionsContacts(ManagementContacts AccessMC)
        {
            ReadFile(AccessMC);

            List<Phones> OptionsPhone = new List<Phones>();


            while (ExitContacts == false)
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

                    int NumberID = CalculateID(AccessMC);

                    try
                    {
                        Contacts AccessContacts = new Contacts(NumberID, Name, Address, Email, OptionsPhone);
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
                if (Choose == "5")
                {
                    WriteFile(AccessMC.AddContacts);
                    ExitContacts = true;
                }
            }
        }
    }
}
