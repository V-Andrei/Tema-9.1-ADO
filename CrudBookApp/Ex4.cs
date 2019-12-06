using System;
using System.Data.SqlClient;

namespace CrudBookApp
{
    class Ex4
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=HomeworkWeek9Day1;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            try
            {
                //PrintInsertId(connection);
                //UpdateBook(connection);
                //DeleteBook(connection);
                SelectBook(connection);

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

        private static void SelectBook(SqlConnection connection)
        {

            try
            {
                Console.WriteLine("Enter id for book to select: ");
                var id = Console.ReadLine();

                var query = "select * from Book where BookId = @idSelectParam";

                SqlParameter param = new SqlParameter("@idSelectParam", id);

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add(param);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var idSelect = currentRow["BookId"];
                    var title = currentRow["Title"];

                    Console.WriteLine($"{idSelect} - {title}");

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void DeleteBook(SqlConnection connection)
        {
            connection.Open();

            try
            {
                Console.WriteLine("Enter id for book to delete: ");
                var id = Console.ReadLine();

                var command = "delete from Book where Bookid = @PubIdParam";

                SqlParameter param = new SqlParameter("@PubIdParam", id);

                SqlCommand deleteCommand = new SqlCommand(command, connection);

                deleteCommand.Parameters.Add(param);

                deleteCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
           
        }

        private static void UpdateBook(SqlConnection connection)
        {
            connection.Open();

            int newBookId;
            string newBookTitle;

            try
            {
                Console.WriteLine("Update the Book Id");
                newBookId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Update the name of Publisher: ");
                newBookTitle = Console.ReadLine();

                var commandQuery = "update Book set Title = @titleParam where BookId = @idParam";

                SqlParameter newBookIdParam = new SqlParameter("@idParam", newBookId);
                SqlParameter newBookTitleParam = new SqlParameter("@titleParam", newBookTitle);

                SqlCommand updateCommand = new SqlCommand(commandQuery, connection);

                updateCommand.Parameters.Add(newBookIdParam);
                updateCommand.Parameters.Add(newBookTitleParam);

                updateCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        private static void PrintInsertId(SqlConnection connection)
        {

            connection.Open();
            
            string titile = "Inclestarea regilor 2";
            int publisherId = 2;
            int year = 2010;
            int price = 49;

            try
            {
                var commandQuery = "insert into Book (PublisherId, Title, Year, Price) values (@PublisherIdParam, @TitleParam, @YearParam, @PriceParam); select scope_identity();";

                SqlParameter idParam = new SqlParameter("@PublisherIdParam", publisherId);
                SqlParameter titleParam = new SqlParameter("@TitleParam", titile);
                SqlParameter yearParam = new SqlParameter("@YearParam", year);
                SqlParameter priceParam = new SqlParameter("@PriceParam", price);

                SqlCommand insertCommand = new SqlCommand(commandQuery, connection);

                insertCommand.Parameters.Add(idParam);
                insertCommand.Parameters.Add(titleParam);
                insertCommand.Parameters.Add(yearParam);
                insertCommand.Parameters.Add(priceParam);

                var id = insertCommand.ExecuteScalar();

                Console.WriteLine(id);

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
