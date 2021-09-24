using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

            //Chamando o banco de dados
            using (var connection = new SqlConnection(conectionString))
            {
                //CreateManyCategory(connection);
                //GetCategory(connection);
                //CreateCategory(connection);
                //UpdateCategory(connection);
                //DeleteCategory(connection);
                //ListCategories(connection);
                //ExecuteProcedure(connection);
                //ExecuteReadProcedure(connection);
                //ExecuteScalar(connection);
                //ReadView(connection);
                //OneToOne(connection);
                //OneToMany(connection);
                //QueryMultiple(connection);
                //SelectIn(connection);
                //Like(connection, "api");
                //Transaction(connection);

            }
        }

        static void ListCategories(SqlConnection connection)
        {
            //Fazendo uma consulta na tabela Category
            var categories = connection.Query<Category>("SELECT [ID],[TITLE] FROM [Category]");
            foreach (var item in categories)
            {
                System.Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        static void GetCategory(SqlConnection connection)
        {
            var category = connection
                .QueryFirstOrDefault<Category>(
                    "SELECT TOP 1 [Id], [Title] FROM [Category] WHERE [Id]=@id",
                    new
                    {
                        id = "af3407aa-11ae-4621-a2ef-2028b85507c4"
                    });
            Console.WriteLine($"{category.Id} - {category.Title}");

        }

        static void CreateCategory(SqlConnection connection)
        {
            // Criando o Objeto categoria >>> Adicionando dados à tabela Category
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a servicos do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            // Criando a Query para insetir dados na tabela Category << Nunca concatenar string no insert; update; select
            var insertSql = @"INSERT INTO 
                    [Category] 
                VALUES(
                    @Id, 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, @Description, @Featured)";

            //Fazendo uma inserção de linha na tabela Category
            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            System.Console.WriteLine($"{rows} linhas inseridas");
        }

        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@Id";
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
                title = "Frontend 2021"
            });
            System.Console.WriteLine($"{rows} registros atualizados");
        }

        static void DeleteCategory(SqlConnection connection)
        {
            var deleteQuery = "DELETE [Category] WHERE [Id]=@id";
            var rows = connection.Execute(deleteQuery, new
            {
                id = new Guid("ea8059a2-e679-4e74-99b5-e4f0b310fe6f"),
            });

            Console.WriteLine($"{rows} registros excluídos");
        }

        static void CreateManyCategory(SqlConnection connection)
        {
            // Criando o Objeto categoria >>> Adicionando dados à tabela Category
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a servicos do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            // Criando o Objeto categoria >>> Adicionando dados à tabela Category
            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria Nova";
            category2.Url = "categoria-nova";
            category2.Description = "Categoria destinada a categoria nova";
            category2.Order = 9;
            category2.Summary = "Categoria";
            category2.Featured = true;

            // Criando a Query para insetir dados na tabela Category << Nunca concatenar string no insert; update; select
            var insertSql = @"INSERT INTO 
                    [Category] 
                VALUES(
                    @Id, 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, @Description, @Featured)";

            //Fazendo uma inserção de linha na tabela Category
            var rows = connection.Execute(insertSql, new[]{
                new
                            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            },
            new
                                       {
                category2.Id,
                category2.Title,
                category2.Url,
                category2.Summary,
                category2.Order,
                category2.Description,
                category2.Featured
            }
            });
            System.Console.WriteLine($"{rows} linhas inseridas");
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var procedure = "[spDeleteStudent]";
            var pars = new { StudentId = "92a13496-9032-4a6a-aa76-b921115c5923" };
            var affectedRows = connection.Execute(
                procedure,
                pars,
                commandType: CommandType.StoredProcedure);

            System.Console.WriteLine($"{affectedRows} linha afetada");
        }

        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "[spGetCoursesByCategory]";
            var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
            var couses = connection.Query<Category>(
                procedure,
                pars,
                commandType: CommandType.StoredProcedure);

            foreach (var item in couses)
            {
                System.Console.WriteLine(item.Id);
            }
        }

        static void ExecuteScalar(SqlConnection connection)
        {
            // Criando o Objeto categoria >>> Adicionando dados à tabela Category
            var category = new Category();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a servicos do AWS";
            category.Order = 10;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            // Criando a Query para insetir dados na tabela Category << Nunca concatenar string no insert; update; select
            var insertSql = @"
                INSERT INTO 
                    [Category] 
                OUTPUT inserted.[Id]
                VALUES(
                    NEWID(), 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, @Description, @Featured)";

            //Fazendo uma inserção de linha na tabela Category
            var id = connection.ExecuteScalar<Guid>(insertSql, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            System.Console.WriteLine($"A categoria escolhida foi: {id}");
        }

        static void ReadView(SqlConnection connection)
        {
            var sql = "SELECT * FROM [vwCourses]";
            var courses = connection.Query(sql);

            foreach (var item in courses)
            {
                System.Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        static void OneToOne(SqlConnection connection)
        {
            var sql = @"
            SELECT
                *
            FROM
                [CareerItem]
            INNER JOIN
                [Course] ON [CareerItem].[CourseId] = [Course].[Id]";

            var items = connection.Query<CareerItem, Course, CareerItem>(
                sql,
                (careerItem, course) =>
                {
                    careerItem.Course = course;
                    return careerItem;
                }, splitOn: "Id");

            foreach (var item in items)
            {
                System.Console.WriteLine($"{item.Title} - Curso: {item.Course.Title}");
            }
        }

        static void OneToMany(SqlConnection connection)
        {
            var sql = @"
                SELECT 
                    [Career].[Id],
                    [Career].[Title],
                    [CareerItem].[CareerId],
                    [CareerItem].[Title]
                FROM 
                    [Career] 
                INNER JOIN 
                    [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
                ORDER BY
                    [Career].[Title]";

            var careers = new List<Career>();
            var items = connection.Query<Career, CareerItem, Career>(
                sql,
                (career, item) =>
                {
                    var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();
                    if (car == null)
                    {
                        car = career;
                        car.Items.Add(item);
                        careers.Add(car);
                    }
                    else
                    {
                        car.Items.Add(item);
                    }

                    return career;
                }, splitOn: "CareerId");

            foreach (var career in careers)
            {
                System.Console.WriteLine($"{career.Title}");
                foreach (var item in career.Items)
                {
                    System.Console.WriteLine($" - {item.Title}");
                }
            }
        }

        static void QueryMultiple(SqlConnection connection)
        {
            var query = "SELECT * FROM [Category]; SELECT * FROM [Course]";

            using (var multi = connection.QueryMultiple(query))
            {
                var categories = multi.Read<Category>();
                var courses = multi.Read<Course>();

                foreach (var item in categories)
                {
                    System.Console.WriteLine(item.Title);
                }
                foreach (var item in courses)
                {
                    System.Console.WriteLine(item.Title);
                }
            }
        }

        static void SelectIn(SqlConnection connection)
        {
            var query = @"
                SELECT TOP 10 * FROM Career WHERE [Id] IN @Id";


            var items = connection.Query<Career>(query, new
            {
                Id = new[]{
                    "e6730d1c-6870-4df3-ae68-438624e04c72",
                    "4327ac7e-963b-4893-9f31-9a3b28a4e72b"
                }
            });

            foreach (var item in items)
            {
                System.Console.WriteLine(item.Title);
            }
        }

        static void Like(SqlConnection connection, string term)
        {
            var query = @"
                SELECT *FROM [Course] WHERE [Title] LIKE @exp";


            var items = connection.Query<Course>(query, new
            {
                exp = $"%{term}%"
            });

            foreach (var item in items)
            {
                System.Console.WriteLine(item.Title);
            }
        }

        static void Transaction(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Minha categoria que não";
            category.Url = "amazon";
            category.Description = "Categoria destinada a servicos do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertSql = @"INSERT INTO 
                    [Category] 
                VALUES(
                    @Id, 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, @Description, @Featured)";
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                var rows = connection.Execute(insertSql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                }, transaction);

                transaction.Commit();
                //transaction.Rollback();

                System.Console.WriteLine($"{rows} linhas inseridas");
            }

        }

    }

}



