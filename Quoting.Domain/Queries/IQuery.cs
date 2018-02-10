using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quoting.Domain.Queries
{
    public interface IQuery<TResult>
    {
        Task<IEnumerable<TResult>> Run();
    }
    public interface IQuery<TResult, TRequest>
    {
        Task<IEnumerable<TResult>> Run(TRequest request);
    }
}
