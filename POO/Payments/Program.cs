using System;

namespace Payments
{
    class Program
    {
        static void Main(string[] args)
        {
            var pagamentoBoleto = new PagamentoBoleto();
            Console.WriteLine("Hello World!");
        }

        public class Pagamento
        {
            //Propriedades
            public DateTime Vencimento;
            //Métodos
            public void Pagar()
            {

            }

        }
        public class PagamentoBoleto : Pagamento
        {
            public DateTime Vencimento;

            public void pagar() { }
        }
    }
}
