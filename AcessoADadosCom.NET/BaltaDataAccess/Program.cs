using System;
using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BaltaDataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            //Conexão com banco de dados SQL Server
            const string conectionString = "Server=localhost; Database = balta; Integrated Security=SSPI";

            using (var connection = new SqlConnection(conectionString))
            {
                var categories = connection.Query<Category>("SELECT [ID],[TITLE] FROM [Category]");
                foreach (var category in categories)
                {
                    System.Console.WriteLine($"{category.Id} - {category.Title}");
                }
            }
        }
    }
}
