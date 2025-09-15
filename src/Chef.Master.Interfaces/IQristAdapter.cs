using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chef.Master.Interfaces
{
    public interface IQristAdapter
    {
        Task<string> GenerateQristCodeAsync(
            string recipeName,
            IEnumerable<string> ingredients,
            CancellationToken cancellationToken
        );
    }
}