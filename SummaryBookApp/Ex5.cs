using System;
using System.Data.SqlClient;

namespace SummaryBookApp
{
    class Ex5
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=HomeworkWeek9Day1;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            try
            {
                //Select2010Books(connection);




            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
            }

            Console.ReadKey();
        }

        private static void Select2010Books(SqlConnection connection)
        {
            try
            {
                var query = "select * from Book where Year = 2010";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var id = currentRow["PublisherId"];
                    var title = currentRow["Title"];

                    Console.WriteLine($"{id} - {title}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
