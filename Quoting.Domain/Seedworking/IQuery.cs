using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quoting.Domain.Seedworking
{
    public interface IQuery<TResult>
    {
        Task<TResult> Run();
    }
    public interface IQuery<TResult, TRequest>
    {
        Task<TResult> Run(TRequest request);
    }
}
