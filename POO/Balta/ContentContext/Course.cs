using System.Collections.Generic;
using Balta.ContentContext.Enums;

namespace Balta.ContentContext
{
    public class Course : Content
    {
        //Construtor
        public Course(string title,string url)
        :base(title,url)
        {
            Modules = new List<Module>(); // Inicializando a lista
        }
        //Propriedades
        public string Tag { get; set; }
        public IList<Module> Modules { get; set; }
        public int DurationInMinutes { get; set; }
        public EcontentLevel Level { get; set; }
    }
}