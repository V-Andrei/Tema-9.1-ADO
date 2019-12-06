using System;
using System.Data.SqlClient;

namespace SummaryPublisherApp
{
    class Ex2
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=HomeworkWeek9Day1;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            try
            {
                //CountPublishers(connection);
                //ReadPublishers(connection);
                //CountBooks(connection);
                //TotalPrice(connection);
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

        private static void TotalPrice(SqlConnection connection)
        {
            try
            {
                int insertPublisherId;

                Console.WriteLine("Insert the Publisher Id for the total price");
                insertPublisherId = Convert.ToInt32(Console.ReadLine());

                var commandQuery = "select PublisherId, sum(Price) as 'Total price' FROM book where PublisherId = @insertPublisherId group by PublisherId";

                SqlParameter paramId = new SqlParameter("@insertPublisherId", insertPublisherId);

                SqlCommand insertCommand = new SqlCommand(commandQuery, connection);

                insertCommand.Parameters.Add(paramId);

                SqlDataReader readQuery = insertCommand.ExecuteReader();

                while (readQuery.Read())
                {
                    var currentRow = readQuery;

                    var id = currentRow["PublisherId"];
                    var numberBooks = currentRow["Total price"];

                    Console.WriteLine($"{id} - {numberBooks}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
        }

        private static void CountBooks(SqlConnection connection)
        {
            try
            {
                var commandQuery = "select Publisher.name, COUNT(*) AS 'number of books' FROM Publisher, book WHERE Publisher.PublisherId = book.PublisherId GROUP BY Publisher.name";

                SqlCommand countCommand = new SqlCommand(commandQuery, connection);

                SqlDataReader readQuery = countCommand.ExecuteReader();

                while (readQuery.Read())
                {
                    var currentRow = readQuery;

                    var name = currentRow["Name"];
                    var numberBooks = currentRow["number of books"];

                    Console.WriteLine($"{name} - {numberBooks}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void CountPublishers(SqlConnection connection)
        {
            try
            {
                var commandQuery = "select count(PublisherId) from Publisher";

                SqlCommand countCommand = new SqlCommand(commandQuery, connection);

                var count = countCommand.ExecuteScalar();

                Console.WriteLine($"Count is: {count}");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ReadPublishers(SqlConnection connection)
        {
            try
            {
                string readPublisher = "select * from Publisher where PublisherId < 10";

                SqlCommand command = new SqlCommand(readPublisher, connection);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var publisherId = currentRow["PublisherId"];
                    var name = currentRow["Name"];

                    Console.WriteLine($"{publisherId} - {name}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
