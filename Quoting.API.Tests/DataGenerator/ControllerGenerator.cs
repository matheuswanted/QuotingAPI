using Quoting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.API.Tests.DataGenerator
{
    public class ControllerGenerator
    {
        public static Quote OkQuote()
        {
            return new Quote()
            {
                Id = 1,
                Customer = OkCustomer(),
                Vehicle = new Vehicle
                {
                    Make = "Ford",
                    Model = "Fiesta",
                    Type = "Car",
                    ManufacturingYear = 2017
                }
            };
        }
        public static Customer OkCustomer()
        {
            var c = new Customer()
            {
                Id = 1,
                SSN = "123-25-256",
                Address = "15th street",
                BirthDate = DateTime.Now,
                Email = "email@gmail.com",
                Gender = "M",
                Phone = "+18695972"
            };
            c.AddVehicle(new Vehicle
            {
                Make = "Ford",
                Model = "Fiesta",
                Type = "Car",
                ManufacturingYear = 2017
            });
            return c;
        }
        public static IEnumerable<object[]> BadCustomers()
        {
            yield return new[] { new Quote() };
            yield return new[] { new Quote()
                {
                    Customer = new Customer
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
