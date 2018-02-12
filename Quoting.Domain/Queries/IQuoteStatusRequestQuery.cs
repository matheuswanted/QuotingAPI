using Quoting.Domain.Models;
using Quoting.Domain.Seedworking;

namespace Quoting.Domain.Queries
{
    public interface IQuoteStatusRequestQuery : IQuoteQuery, IQuery<Quote, int>
    {

    }
}
