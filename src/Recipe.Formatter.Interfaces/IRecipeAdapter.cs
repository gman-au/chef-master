using System.Threading;
using System.Threading.Tasks;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Interfaces
{
    public interface IRecipeAdapter
    {
        AdapterMetadataViewModel Metadata { get; set; }

        Task<RecipeParseResponseViewModel> ProcessAsync(
            RecipeParseRequestViewModel request,
            CancellationToken cancellationToken = default);
    }
}