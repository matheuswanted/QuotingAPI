using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Seedworking
{
    class DefaultInconsistency : IModelInconsistency
    {
        public DefaultInconsistency(string notifcation, bool error = true)
        {
            this.Message = notifcation;
            this.Error = error;
        }
        public bool Error { get; }

        public string Message { get; }
    }
}
