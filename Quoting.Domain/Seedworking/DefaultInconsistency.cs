using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Seedworking
{
    class DefaultInconsistency : IModelInconsistency
    {
        public DefaultInconsistency(string notifcation, bool error = true)
        {
            this.Notification = notifcation;
            this.Error = error;
        }
        public bool Error { get; }

        public string Notification { get; }
    }
}
