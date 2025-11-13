using System;
using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.FileIO;

namespace exercise_schedule{
    class Program()
    {
        // Making database connection string
        static string connectionString = @"Data Source=exercise-schedule";
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
            
        }

        private static void Insert()
        {
            
        }

        private static void Delete()
        {
            
        }

        private static void Update()
        {
            
        }


    }

    
}