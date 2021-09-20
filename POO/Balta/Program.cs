using System;
using System.Collections.Generic;
using System.Linq;
using Balta.ContentContext;

namespace Balta
{
    class Program
    {
        static void Main(string[] args)
        {
            var articles = new List<Article>();
            articles.Add(new Article("Artigo sobre OOP", "Orientacao-objetos"));
            articles.Add(new Article("Artigo sobre C#", "csharp"));
            articles.Add(new Article("Artigo sobre .NET", "dotnet"));

            // foreach(var article in articles){
            //     Console.WriteLine(article.Id);
            //     Console.WriteLine(article.Title);
            //     Console.WriteLine(article.Url);
            // }

            var courses = new List<Course>();
            var courseOOP = new Course("Fundamentos OOP", "Fundamentos-oop");
            var courseCsharp = new Course("Fundamentos C#", "Fundamentos-C#");
            var courseAspNet = new Course("Fundamentos ASP.NET", "Fundamentos-ASP.NET");

            courses.Add(courseOOP);
            courses.Add(courseCsharp);
            courses.Add(courseAspNet);

            var careers = new List<Career>();
            var careeerDotNet = new Career("Especialista .NET", "Especialista-dotnet");
            var careerItem2 = new CareerItem(2, "Aprenda OOP", "", null);
            var careerItem = new CareerItem(1, "Comece por aqui", "", null);
            var careerItem3 = new CareerItem(3, "Aprenda .NET", "", null);
            
            careeerDotNet.Items.Add(careerItem2);
            careeerDotNet.Items.Add(careerItem);
            careeerDotNet.Items.Add(careerItem3);
            careers.Add(careeerDotNet);

            foreach (var career in careers)
            {
                Console.WriteLine(career.Title);
                foreach (var item in career.Items.OrderBy(x => x.Order)) //Ordenacao de listas
                {
                    Console.WriteLine($"{item.Order} - {item.Title}");
                }
            }
        }
    }
}
