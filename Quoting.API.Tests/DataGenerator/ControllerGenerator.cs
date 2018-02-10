﻿using Quoting.Domain.Models;
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
                Customer = new Customer()
                {
                    SSN = "123-25-256",
                    Address = "15th street",
                    BirthDate = DateTime.Now,
                    Email = "email@gmail.com",
                    Gender = "M",
                    Phone = "+18695972"
                },
                Vehicle = new Vehicle
                {
                    Maker = "Ford",
                    Model = "Fiesta",
                    Type = "Car",
                    ManufacturingYear = 2017
                }
            };
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
