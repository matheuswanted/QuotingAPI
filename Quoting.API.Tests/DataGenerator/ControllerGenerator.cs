using Moq;
using Quoting.Domain.Models;
using Quoting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.API.Tests.DataGenerator
{
    public class ControllerGenerator
    {
        public static QuoteRequest OkQuote()
        {
            var m = new Mock<QuoteRequest>();
            m.Object.Customer = OkCustomer();
            m.Object.Vehicle = new QuoteRequestVehicle
            {
                Make = "Ford",
                Model = "Fiesta",
                Type = "Car",
                ManufacturingYear = 2017
            };
            m.Setup(r => r.ToQuote())
                .Returns(() => new Quote() { Id = 1 }.From(m.Object));

            return m.Object;
        }
        public static QuoteRequestCustomer OkCustomer()
        {
            return new QuoteRequestCustomer()
            {
                SSN = "123-25-256",
                Address = "15th street",
                BirthDate = DateTime.Now,
                Email = "email@gmail.com",
                Gender = "M",
                Phone = "+18695972"
            };
        }
        public static IEnumerable<object[]> BadCustomers()
        {
            yield return new[] { new QuoteRequest() };
            yield return new[] { new QuoteRequest()
                {
                    Customer = new QuoteRequestCustomer
                    {
                        SSN = "123-25-256",
                        Address = "15th street",
                        BirthDate = DateTime.Now,
                        Email = "email@gmail.com",
                        Gender = "M",
                        Phone = "+18695972"
                    }
                }
            };
        }
    }
}
