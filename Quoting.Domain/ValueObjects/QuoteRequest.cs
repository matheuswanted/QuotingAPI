using Quoting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.ValueObjects
{
    public class QuoteRequestCustomer
     {
        public string SSN { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public class QuoteRequestVehicle
    {
        public string Type { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int ManufacturingYear { get; set; }
    }
    public class QuoteRequest
    {
        public QuoteRequestCustomer Customer { get; set; }
        public QuoteRequestVehicle Vehicle { get; set; }
        public virtual Quote ToQuote()
            => new Quote().From(this);
    }
}
