using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Models
{
    public class Quote : ConsistentModel, IAggregateRoot
    {
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }

        public override bool IsConsistent()
        {
            ResetModelState();
            CheckChildConsistency(Customer, "Customer");
            CheckChildConsistency(Vehicle, "Vehicle");

            return base.IsConsistent();
        }
    }
}
