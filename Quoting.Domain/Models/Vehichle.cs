using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoting.Domain.Models
{
    public class Vehicle : ConsistentModel
    {
        public string Type { get; set; }
        public int ManufacturingYear { get; set; }
        public string Model { get; set; }
        public string Maker { get; set; }
        public override bool IsConsistent()
        {
            ResetModelState();
            if (string.IsNullOrEmpty(Type))
                AddInconsitency("Invalid vehicle type.");
            if (ManufacturingYear < 1700 && ManufacturingYear > DateTime.Now.Year)
                AddInconsitency("Invalid Manufacturing Year.");
            if (string.IsNullOrEmpty(Model))
                AddInconsitency("Invalid vehicle model");
            if (string.IsNullOrEmpty(Maker))
                AddInconsitency("Invalid vehicle maker.");
            return base.IsConsistent();
        }
    }
}
