using System.Threading.Tasks;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Adapters.SchemaOrg.Factories
{
    public interface IResponseFactory
    {
        Task<RecipeViewModel> ForAsync(Schema.NET.Recipe recipe);
    }
}