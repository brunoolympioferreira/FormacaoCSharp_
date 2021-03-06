using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValuieObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using System;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            //Verificar se documento já esta cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            //Verificar se email já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Email", "Este E-mail já está em uso");

            //Gerar Vos
             var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Enums.EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,command.Number,command.Nighborhood,command.City,command.State,command.Country,command.ZipCode);


            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate,
                command.ExpireDate, command.Total, command.TotalPaid, command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address, email);
            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Checar as notificações
            if (Invalid)
                return new CommandResult(false, "Nao foi possivel realizar sua assinatura");

            //Salvar as Informações
            _repository.CreateSubscription(student);

            //Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Balta.Io", "Sua assinatura foi realizada ");
            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Verificar se documento já esta cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            //Verificar se email já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Email", "Este E-mail já está em uso");

            //Gerar Vos
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Enums.EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Nighborhood, command.City, command.State, command.Country, command.ZipCode);


            //Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.TransactionCode, command.PaidDate,
                command.ExpireDate, command.Total, command.TotalPaid, command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address, email);
            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as Informações
            _repository.CreateSubscription(student);

            //Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Balta.Io", "Sua assinatura foi realizada ");
            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
