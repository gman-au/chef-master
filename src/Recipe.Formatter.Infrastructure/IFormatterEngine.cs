using System.Threading.Tasks;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Infrastructure
{
    public interface IFormatterEngine
    {
        Task<RecipeParseResponseViewModel> ProcessAsync(RecipeParseRequestViewModel request);
    }
}