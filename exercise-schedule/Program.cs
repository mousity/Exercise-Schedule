using System;
using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.FileIO;

namespace exercise_schedule{
    class Program()
    {
        // Making database connection string
        static string connectionString = @"Data Source=exercise-schedule.db";
        static void Main(string[] args)
        {
            
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText = @"CREATE TABLE IF NOT EXISTS exercise_schedule(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ""Date"" TEXT,
                Quantity INTEGER)";

                tableCommand.ExecuteNonQuery();
                connection.Close();
            }

            GetUserInput();

        }

        static void GetUserInput()
        {
            Console.Clear();
            bool closeApp = false;
            
            while(!closeApp){
                Console.WriteLine("\nWelcome to the exercise schedule!");
                Console.WriteLine("Here, the purpose is the record your previous exercises, by date and number of minutes spent exercising\n");

                Console.WriteLine("Select an option from the menu below by typing the number:");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("0: Quit program");
                Console.WriteLine("1: View all records");
                Console.WriteLine("2: Insert record");
                Console.WriteLine("3: Delete record");
                Console.WriteLine("4: Update record");


                int input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 0:
                        closeApp = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    case 1:
                        GetAllRecords();
                        break;
                    case 2:
                        Insert();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        Update();
                        break;
                    default:
                        Console.WriteLine("Your input was not a number 0-4. Try again!");
                        break;
                }



            }

        }

        private static void GetAllRecords()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText = $"SELECT * FROM exercise_schedule";

                List<ExerciseSchedule> tableData = new List<ExerciseSchedule>();
                SqliteDataReader reader = tableCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new ExerciseSchedule
                            {
                                Id = reader.GetInt32(0),
                                Date = DateTime.Parse(reader.GetString(1)),
                                Quantity = reader.GetInt32(2)
                            }
                        );
                    }
                } else
                {
                    Console.WriteLine("\nNo rows to retrieve!\n");
                }

                connection.Close();
                Console.WriteLine("-------------------------------------");
                foreach(var entry in tableData)
                {
                    Console.WriteLine($"Id - {entry.Id} | Date - {entry.Date} | Minutes - {entry.Quantity}");
                }
                Console.WriteLine("-------------------------------------");
            }
        }

        private static void Insert()
        {
            string? date = GetDate();
            int? minutes = GetMinutes();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText = $"INSERT INTO exercise_schedule('Date', Quantity) VALUES ($date, $quantity)";
                tableCommand.Parameters.AddWithValue("$date", date.ToString());
                tableCommand.Parameters.AddWithValue("$quantity", minutes);

                tableCommand.ExecuteNonQuery(); 


                connection.Close();
            }
        }

        internal static string? GetDate()
        {
            Console.WriteLine("Please enter a date you exercised in dd-mm-yy format. Type 0 to exit to the main menu.");
            string input = Console.ReadLine();
            return input;
        }

        internal static int? GetMinutes()
        {
            Console.WriteLine("Please enter the amount of minutes you exercised on that day. Type 0 to exit to the main menu.");
            int input = Convert.ToInt32(Console.ReadLine());
            return input;
        }

        private static void Delete()
        {
            Console.WriteLine("\nPlease pick the ID of the record you'd like to delete\n");
        }

        private static void Update()
        {
            
        }


    }

    public class ExerciseSchedule
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
    }
}