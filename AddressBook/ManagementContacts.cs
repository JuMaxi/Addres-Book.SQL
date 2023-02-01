using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class ManagementContacts
    {
        string ConnectionString = "Server=LAPTOP-P4GEIO8K\\SQLEXPRESS;Database=AddressBook;User Id=sa;Password=S4root;";

        public void AddNewContact(Contacts NewContact)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string insert = "insert into Contact (Name, Address, Email) values('" + NewContact.Name + "','" + NewContact.Address + "','" + NewContact.Email.EmailAddress + "')";
                SqlCommand command = new SqlCommand(insert, connection);

                connection.Open();
                command.ExecuteNonQuery();

                PhoneMenu PhoneMenu = new PhoneMenu();
                PhoneMenu.AddPhonesDB(NewContact.Phones);
            }
        }
        public void ShowContacts()
        {
            Menu Access = new Menu();

            Access.WriteNameCompany();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string select = "select Contact.Id, Contact.Name, Contact.Address, Contact.Email, Phones.Kind, Phones.Phone from Contact left join Phones on Contact.Id = Phones.ContactId";

                SqlCommand command = new SqlCommand(select, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                string KindPhone = "";
                int IDActual = 0;

                while (reader.Read())
                {
                    int IDReader = Convert.ToInt32(reader["Id"]);

                    if (IDActual != IDReader)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("ID Number: " + reader["Id"]);
                        Console.WriteLine("Name: " + reader["Name"]);
                        Console.WriteLine("Address: " + reader["Address"]);
                        Console.WriteLine("Email: " + reader["Email"]);
                    }
                    IDActual = IDReader;

                    if (reader["Phone"] != null)
                    {
                        if (reader["Kind"].ToString() == "1")
                        {
                            KindPhone = "Mobile Phone";
                        }
                        else
                        {
                            if (reader["Kind"].ToString() == "2")
                            {
                                KindPhone = "Home Phone";
                            }
                            else
                            {
                                if (reader["Kind"].ToString() == "3")
                                {
                                    KindPhone = "Business Phone";
                                }

                            }
                        }
                        Console.WriteLine(KindPhone + ": " + reader["Phone"]);
                    }
                }
            }
        }

        public void RemoveContacts(int NumberID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string DeletePhones = "delete from Phones where ContactId=" + NumberID;
                SqlCommand command = new SqlCommand(DeletePhones, connection);
                connection.Open();

                command.ExecuteNonQuery();

                string DeleteContact = "delete from Contact where Id=" + NumberID;
                command = new SqlCommand(DeleteContact, connection);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateContacts(int NumberID, string Table, string Change, string New)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string Update = "update " + Table + " set " + Change + "=" + "'" + New + "'" + "where Id=" + NumberID;

                SqlCommand command = new SqlCommand(Update, connection);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }

    }
}






