using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoting.Domain.Seedworking
{
    public interface IConsistentModel
    {
        int Id { get; set; }

        bool IsConsistent();
        IReadOnlyCollection<IModelInconsistency> ModelInconsistecies { get; }
    }
}
