using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class Contacts
    {
        public string Name;
        public string Address;
        public Email Email;
        public List<Phones> Phones;
        

        public Contacts(string NamePerson, string AddressPerson, string EmailPerson, List<Phones> New)
        {
            Email EmailValidate = new Email(EmailPerson);
            
            Name = NamePerson;
            Address = AddressPerson;
            Email = EmailValidate;
            Phones = New;

            ValidateContacts();
        }

        public void ValidateContacts()
        {
            string NameTrim = Name.Trim();
            Name = NameTrim;

            if (Name.IndexOf(" ") < 0)
            {
                throw new Exception("In this field you need to put the Name + Last Name.");
            }
            if (Name == " ")
            {
                throw new Exception("This field is Mandatory. You need to put the Name + Last Name for to complet this registration.");
            }
            if ((Address.Length == 0)
                && (Phones.Count == 0))
            {
                Console.WriteLine(" ");
                throw new Exception("You need to put at least one field more to complet this registration");
            }
        }
    }
}

