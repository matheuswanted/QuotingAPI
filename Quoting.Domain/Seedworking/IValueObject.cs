using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Seedworking
{
    public interface IValueObject
    {
        bool Same(IValueObject @object);
    }
}
