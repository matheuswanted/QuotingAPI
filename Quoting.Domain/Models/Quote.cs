using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Models
{
    public enum QuoteStatus
    {
        Requested,
        Done
    }
    public class Quote : ConsistentModel, IAggregateRoot
    {
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
        public decimal Value { get;}
        public QuoteStatus Status{ get; }
        public override bool IsConsistent()
        {
            ResetModelState();
            CheckChildConsistency(Customer, "Customer");
            CheckChildConsistency(Vehicle, "Vehicle");

            return base.IsConsistent();
        }

        public void SetCustomer(Customer customer)
        {
            Customer = customer;
            Vehicle = customer.CurrentVehicle;
        }
    }
}
