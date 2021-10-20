﻿using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValuieObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string nighborhood, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            Nighborhood = nighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Street,3,"Address.Street","A rua deve conter pelo menos 3 caracteres")   
                );
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Nighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}
