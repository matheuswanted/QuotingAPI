using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoting.Domain.Models
{
    public class Customer : ConsistentModel
    {
        public string SSN { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime BirthDate { get; set; }

        public override bool IsConsistent()
        {
            ResetModelState();

            if (string.IsNullOrEmpty(SSN))
                AddInconsitency("Invalid SSN.");
            if (Vehicle == null)
                AddInconsitency("Invalid Vehicle.");
            else if (!Vehicle.IsConsistent())
                AddInconsitencies(Vehicle.ModelInconsistecies);

            return base.IsConsistent();
        }
    }
}
