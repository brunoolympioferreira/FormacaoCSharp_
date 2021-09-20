using System.Collections.Generic;
using Balta.SharedContext;

namespace Balta.ContentContext
{

    public class Module : Base
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