using System;
using System.Data.SqlClient;

namespace InsertPublisherApp
{
    class Ex1
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=HomeworkWeek9Day1;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();


            try
            {               
                InsertPublisher(connection);
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

                Console.WriteLine("The id is: " + newPublisherId);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }       
    }
}
