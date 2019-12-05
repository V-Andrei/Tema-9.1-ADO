using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace InsertPublisherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=HomeworkWeek9Day1;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);           

            try
            {
                ReadPublishers(connection);
                InsertPublisher(connection);
                ReadPublishers(connection);

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

        private static void InsertPublisher(SqlConnection connection)
        {
            connection.Open();
            try
            {
                string newPublisherName;
                int newPublisherId;

                Console.WriteLine("Insert the Publisher Id");
                newPublisherId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Insert the name of Publisher: ");
                newPublisherName = Console.ReadLine();

                var commandQuery = "insert into dbo.Publisher ([PublisherId],[Name]) values ( @newPublisherId , @newPublisherName )";

                SqlParameter paramId = new SqlParameter("@newPublisherId", newPublisherId);
                SqlParameter paramName = new SqlParameter("@newPublisherName", newPublisherName);

                SqlCommand insertCommand = new SqlCommand(commandQuery, connection);

                insertCommand.Parameters.Add(paramId);
                insertCommand.Parameters.Add(paramName);

                insertCommand.ExecuteScalar();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            
        }

        private static void ReadPublishers(SqlConnection connection)
        {
            connection.Open();
            try
            {
                string readPublisher = "select * from Publisher";

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
            finally
            {
                connection.Close();
            }
        }
    }
}
