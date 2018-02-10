using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoting.Domain.Models
{
    public class Vehicle : ConsistentModel, IValueObject
    {
        public string Type { get; set; }
        public int ManufacturingYear { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public override bool IsConsistent()
        {
            ResetModelState();
            if (string.IsNullOrEmpty(Type))
                AddInconsitency("Invalid vehicle type.");
            if (ManufacturingYear < 1700 && ManufacturingYear > DateTime.Now.Year)
                AddInconsitency("Invalid Manufacturing Year.");
            if (string.IsNullOrEmpty(Model))
                AddInconsitency("Invalid vehicle model");
            if (string.IsNullOrEmpty(Make))
                AddInconsitency("Invalid vehicle maker.");
            return base.IsConsistent();
        }
        public bool Same(IValueObject @object)
        {
            var vehicle = @object as Vehicle;
            return vehicle != null && Type == vehicle.Type && ManufacturingYear == vehicle.ManufacturingYear && Model == vehicle.Model && Make == vehicle.Make;
        }
    }
}
