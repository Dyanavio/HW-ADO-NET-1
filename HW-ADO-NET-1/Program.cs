using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_ADO_NET_1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = "Data Source=THERION\\SQLEXPRESS;Initial Catalog = Provision; Integrated Security=True";
                //string connectionString = "";
                var table = GetRecords(connectionString);
                if (table.Rows.Count == 0) throw new Exception("No records or unsuccessful connection attempt");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Successfully received data from database: Provision");
                Console.ResetColor();
                OutputRecords(table);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }
        private static void OutputRecords(DataTable table)
        {
            Console.WriteLine("\n\t\t===== LIST OF ITEMS =====");
            foreach(DataRow row in table.Rows)
            {
                if ((string)row["type"] == "Fruit") Console.ForegroundColor = ConsoleColor.DarkYellow;
                else Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"Name: {row["Name"]} | Type: {row["Type"]} | Color: {row["Color"]} | Calories: {row["Calories"]}");
            }
            Console.ResetColor();
        }

        private static DataTable GetRecords(string connectionString)
        {
            List<string> records = new List<string>();
            DataTable table = new DataTable();
            string query = "Select Name, Type, Color, Calories from Items";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(table);
                }
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
            return table;
        }
    }
}
