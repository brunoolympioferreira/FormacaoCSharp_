using System.Collections.Generic;

namespace Balta.ContentContext
{

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
}