using System;

namespace Balta.ContentContext
{
    public abstract class Content
    {
        //Construtor
        public Content(string tittle, string url)
        {
            Id = Guid.NewGuid(); // Guid vai gerar uma hash de 36 caracteres
            Title = tittle;
            Url = url;
        }
        //Propriedades
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}