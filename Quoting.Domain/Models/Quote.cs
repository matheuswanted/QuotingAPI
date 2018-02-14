using Quoting.Domain.Models.Events;
using Quoting.Domain.Seedworking;
using Quoting.Domain.ValueObjects;
using System;

namespace Quoting.Domain.Models
{
    public enum QuoteStatus
    {
        None,
        Requested,
        Done
    }
    public class Quote : ConsistentModel, IAggregateRoot
    {
        public QuoteRequest Request { get; private set; }
        public Customer Customer { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public decimal Value { get; private set; }
        public int _status;
        public QuoteStatus Status { get => (QuoteStatus)_status; set => _status = (int)value; }
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
        public void SetStatusAsRequested()
        {
            if (Status != QuoteStatus.None)
                throw new ApplicationException("It's not possible to change the current Status to 'Requested'.");
            Status = QuoteStatus.Requested;
            Raise(new QuoteRequestedEventBuilder(this));
        }

        public Quote From(QuoteRequest request)
        {
            Customer = new Customer().From(request.Customer);
            Vehicle = new Vehicle().From(request.Vehicle);
            Request = request;
            Customer.AddVehicle(Vehicle);
            return this;
        }

        public void CalculatePriceWithRules(BasePriceRule basePriceRule, PriceModifierRule modifier)
        {
            Value = basePriceRule.BasePrice * modifier.Modifier;
            Status = QuoteStatus.Done;
        }
    }
}
