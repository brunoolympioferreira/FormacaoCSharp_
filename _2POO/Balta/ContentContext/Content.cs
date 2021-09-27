using System;
using Balta.SharedContext;

namespace Balta.ContentContext
{
    public abstract class Content : Base
    {
        //Construtor
        public Content(string tittle, string url)
        {
            Title = tittle;
            Url = url;
        }
        //Propriedades
        public string Title { get; set; }
        public string Url { get; set; }
    }
}