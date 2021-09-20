using Balta.ContentContext.Enums;

namespace Balta.ContentContext
{

    public class Lecture
    {
        //Propriedades
        public int Ordem { get; set; }
        public string Title { get; set; }

        public int DurationInMinutes { get; set; }
        public EcontentLevel Level { get; set; }
    }
}