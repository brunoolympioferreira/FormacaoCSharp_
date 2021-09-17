using System;
using System.Collections.Generic;

namespace Payments
{
    class Program
    {
        static void Main(string[] args)
        {
            var payments = new List<Payment>();
            payments.Add(new Payment(1));
            payments.Add(new Payment(2));
            payments.Add(new Payment(3));
            payments.Add(new Payment(4));
            payments.Add(new Payment(5));

            foreach (var payment in payments)
            {
                Console.WriteLine(payment.Id);
            }
        }
        public class Payment
        {
            public int Id { get; set; }
            public Payment(int id)
            {
                Id = id;
            }
        }
    }
}
