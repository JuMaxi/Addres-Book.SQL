using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    //PhonesMenu
    public class PhoneMenu
    { 

        Menu Access = new Menu();
        public List<Phones> AddPhones = new List<Phones>();

        public void WriteOptionsPhone()
        {
            Access.WriteNameCompany();

            Console.WriteLine("What Kind of Phone do you want to insert? ");
            Console.WriteLine("1) Mobile Phone: ");
            Console.WriteLine("2) Home Phone: ");
            Console.WriteLine("3) Business Phone: ");
            Console.WriteLine("4) Exit: ");
            Console.Write("--> ");
        }
        public List<Phones> ReadOptionsPhone()
        {
            string TypePhone = "";

            while (TypePhone != "4")
            {
                WriteOptionsPhone();
                TypePhone = Console.ReadLine();

                if ((TypePhone == "1")
                    || (TypePhone == "2")
                    || (TypePhone == "3"))
                {
                    Console.WriteLine(" ");
                    Console.Write("Please, type the number phone: ");
                    string NumberPhone = Console.ReadLine();

                    Phones AccessPhones = new Phones(TypePhone, NumberPhone);

                    AddPhones.Add(AccessPhones);

                    Access.ExitMessage();
                }
            }
            return AddPhones;
        }
        public void AddPhonesDB(List<Phones> ListPhones)
        {
            string ConnectionString = "Server=LAPTOP-P4GEIO8K\\SQLEXPRESS;Database=AddressBook;User Id=sa;Password=S4root;";

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string selectlastId = "select MAX(Id) as Last from Contact";
                SqlCommand command = new SqlCommand(selectlastId, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                int LastID = 0;
                while (reader.Read())
                {
                    LastID = Convert.ToInt32(reader["Last"]);
                }
                reader.Close();

                for (int Position = 0; Position < ListPhones.Count; Position++)
                {
                    string insertPhone = "insert into Phones (ContactId, Kind, Phone) values(" + LastID + ",'" + ListPhones[Position].Type + "','" + ListPhones[Position].Number + "')";
                    
                    SqlCommand commandinsert = new SqlCommand(insertPhone, connection);
                    commandinsert.ExecuteNonQuery();
                }
                
            }
        }
    }
}
