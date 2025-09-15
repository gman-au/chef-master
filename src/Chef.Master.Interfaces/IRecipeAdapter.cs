using System.Threading;
using System.Threading.Tasks;
using Chef.Master.ViewModel;

namespace Chef.Master.Interfaces
{
    public interface IRecipeAdapter
    {
        AdapterMetadataViewModel Metadata { get; set; }

        Task<RecipeParseResponseViewModel> ProcessAsync(
            RecipeParseRequestViewModel request,
            CancellationToken cancellationToken = default);
    }
}