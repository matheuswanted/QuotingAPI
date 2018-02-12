using Quoting.Domain.Models;
using Quoting.Domain.Seedworking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Queries
{
    public interface IQuoteInformationRequestQuery : IQuoteQuery, IQuery<Quote,int>
    {
    }
}
