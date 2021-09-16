using System;

namespace MeuAplicativo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Tratamento de Erros
            Console.Clear();

            var arr = new int[3];

            try
            {
                //    for (var index = 0; index < 10; index++)
                //  {
                //    //System.IndexOutOfRangeException
                //  Console.WriteLine(arr[index]);
                //}
                cadastrar("");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message);
                Console.WriteLine("Falha ao cadastrar o texto");
            }
            finally
            {
                Console.WriteLine("Fim!");
            }

            static void cadastrar(string texto)
            {
                if (string.IsNullOrEmpty(texto))
                {
                    throw new ArgumentException("O texto não pode ser nulo ou vazio");
                }
            }

        }
    }
}
