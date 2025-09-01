using System.Threading.Tasks;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Interfaces
{
    public interface IRecipeAdapter
    {
        Task<RecipeParseResponseViewModel> ProcessAsync(RecipeParseRequestViewModel request);
    }
}