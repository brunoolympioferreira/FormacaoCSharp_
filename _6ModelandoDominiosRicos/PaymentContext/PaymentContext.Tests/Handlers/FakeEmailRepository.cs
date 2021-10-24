using PaymentContext.Domain.Services;

namespace PaymentContext.Tests.Handlers
{
    internal class FakeEmailRepository : IEmailService
    {
        public void Send(string to, string email, string subject, string body)
        {
        }
    }
}