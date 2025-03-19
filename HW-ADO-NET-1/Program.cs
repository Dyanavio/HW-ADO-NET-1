using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Http.Headers;


namespace HW_ADO_NET_1
{
    class Program
    {
        private static readonly string connectionString = "Data Source=THERION\\SQLEXPRESS;Initial Catalog = Provision; Integrated Security=True";
        private static void RunMainMenu()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("------- Provision Database -------");
                Console.ResetColor();
                int input;
                do
                {
                    Console.WriteLine("Options:\n\t1 - Output all\n\t2 - Show min amount of calories\n\t3 - Output products by type\n\t4 - Show all products with cal below indicated\n\t0 - Exit");
                    Console.Write("Input: ");
                    input = Convert.ToInt32(Console.ReadLine());
                    switch (input)
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Exiting . . .");
                            Console.ResetColor();
                            break;
                        case 1:
                            OutputAllRecords();
                            break;
                        case 2:
                            OutputMinCal();
                            break;
                        case 3:
                            Console.Write("Enter type: ");
                            string type = Console.ReadLine();
                            OutputByType(type);
                            break;
                        case 4:
                            Console.Write("Enter calories: ");
                            int cal = Convert.ToInt32(Console.ReadLine());
                            OutputBelowCal(cal);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No such option present");
                            Console.ResetColor();
                            break;
                    }
                }
                while (input != 0);
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(e.Message + ". Closing connection");
            }
        }
        static void Main()
        {
            RunMainMenu();
        }
        private static void OutputBelowCal(int cal)
        {
            try
            {
                using (DbConnection connection = new SqlConnection(connectionString))
                {
                    string query = $"Select * from Items where Items.Calories < {cal}";
                    DbCommand command = new SqlCommand(query, (SqlConnection)connection);

                    connection.Open();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Database connection is open");
                    Console.ResetColor();

                    using(DbDataReader reader = command.ExecuteReader())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Inquired calories amount: '{cal}'");
                        Console.ResetColor();
                        Console.WriteLine("{0, -5} {1, -10} {2, -10} {3, -10} {4, -10}", "Id", "Name", "Type", "Color", "Calories");
                        Console.WriteLine(new string('-', 60));
                        while (reader.Read())
                        {
                            Console.WriteLine("{0, -5} {1, -10} {2, -10} {3, -10} {4, -10}", reader["Id"], reader["Name"], reader["Type"], reader["Color"], reader["Calories"]);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(e.Message + ". Closing connection");
                Console.ResetColor();
            }
        }
        private static void OutputMinCal()
        {
            try
            {
                using(DbConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Select MIN(Items.Calories) from Items";
                    DbCommand command = new SqlCommand(query, (SqlConnection)connection);
                    
                    connection.Open();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Database connection is open");
                    Console.ResetColor();

                    int minCal = Convert.ToInt32(command.ExecuteScalar());
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Minimum of calories present in the table: " + minCal);
                    Console.ResetColor();
                }
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(e.Message + ". Closing connection");
                Console.ResetColor();
            }
        }
        private static void OutputByType(string type)
        {
            try
            {
                if (type != "vegetable" && type != "Fruit")
                {
                    throw new Exception("No such type available exception");
                }
                using (DbConnection connection = new SqlConnection(connectionString))
                {
                    string query = $"Select * From Items Where Type='{type}'";
                    string query2 = $"Select Count(*) from Items Where Items.Type = '{type}'";
                    DbCommand command = new SqlCommand(query, (SqlConnection)connection);
                    DbCommand command2 = new SqlCommand(query2, (SqlConnection)connection);

                    connection.Open();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Database connection is open");
                    Console.ResetColor();

                    int count = Convert.ToInt32(command2.ExecuteScalar());
                    Console.WriteLine("\nCount: " + count);
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Inquired type: '{type}'");
                        Console.ResetColor();
                        Console.WriteLine("{0, -5} {1, -10} {2, -10} {3, -10} {4, -10}", "Id", "Name", "Type", "Color", "Calories");
                        Console.WriteLine(new string('-', 60));
                        while (reader.Read())
                        {
                            Console.WriteLine("{0, -5} {1, -10} {2, -10} {3, -10} {4, -10}", reader["Id"], reader["Name"], reader["Type"], reader["Color"], reader["Calories"]);
                        }
                    }

                }
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(e.Message + ". Closing connection");
                Console.ResetColor();
            }

        }
        private static void OutputAllRecords()
        {
            try
            {
                using(DbConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Select * from Items";
                    DbCommand command = new SqlCommand(query, (SqlConnection)connection);

                    connection.Open();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Database connection is open");
                    Console.ResetColor();

                    using(DbDataReader  reader = (SqlDataReader)command.ExecuteReader())
                    {
                        Console.WriteLine("\n{0, -5} {1, -10} {2, -10} {3, -10} {4, -10}", "Id", "Name", "Type", "Color", "Calories");
                        Console.WriteLine(new string('-', 60));
                        while(reader.Read())
                        {
                            Console.WriteLine("{0, -5} {1, -10} {2, -10} {3, -10} {4, -10}", reader["Id"], reader["Name"], reader["Type"], reader["Color"], reader["Calories"]);
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(e.Message + ". Closing connection");
                Console.ResetColor();
            }
        }
    }
}
