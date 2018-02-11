using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoting.Domain.Models
{
    public class Customer : ConsistentModel, IAggregateRoot
    {
        public Customer()
        {
            _vehicles = new List<Vehicle>();
        }
        public string SSN { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }

        private readonly List<Vehicle> _vehicles;
        public IReadOnlyCollection<Vehicle> Vehicles => _vehicles;

        public DateTime BirthDate { get; set; }
        public int Age => DateTime.Now.Year - BirthDate.Year;

        /// <summary>
        /// The last added vehicle.
        /// </summary>
        public Vehicle CurrentVehicle { get; private set; }
        /// <summary>
        /// Adds an vehicle if it doesn't exist
        /// </summary>
        /// <param name="vehicle"></param>
        public void AddVehicle(Vehicle vehicle)
        {
            var aux = _vehicles.FirstOrDefault(existing => existing.Same(vehicle));
            if (aux == null)
                _vehicles.Add(vehicle);

            CurrentVehicle = aux ?? vehicle;
        }

        public override bool IsConsistent()
        {
            ResetModelState();

            if (string.IsNullOrEmpty(SSN))
                AddInconsitency("Invalid SSN.");
            if (BirthDate == default(DateTime))
                AddInconsitency("Invalid Birth Date.");
            if (string.IsNullOrEmpty(Gender) || !"MF".Contains(Gender))
                AddInconsitency("Invalid Gender.");
            return base.IsConsistent();
        }

    }
}
