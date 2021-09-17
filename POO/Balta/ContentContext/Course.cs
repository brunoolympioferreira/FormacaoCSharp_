using System.Collections.Generic;

namespace Balta.ContentContext
{
    public class Course : Content
    {
        //Construtor
        public Course()
        {
            Modules = new List<Module>(); // Inicializando a lista
        }
        //Propriedades
        public string Tag { get; set; }
        public IList<Module> Modules { get; set; }
    }
    public class Module
    {
        //Construtor
        public Module()
        {
            Lectures = new List<Lecture>(); // Inicializando a lista
        }
        //Propriedades
        public int Order { get; set; }
        public string Title { get; set; }
        public IList<Lecture> Lectures { get; set; }
    }
    public class Lecture
    {
        //Propriedades
        public int Ordem { get; set; }
        public string Title { get; set; }
    }

}