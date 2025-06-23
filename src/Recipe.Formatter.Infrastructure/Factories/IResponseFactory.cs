using System.Threading.Tasks;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Infrastructure.Factories
{
    public interface IResponseFactory
    {
        Task<RecipeViewModel> ForAsync(Schema.NET.Recipe recipe);
    }
}