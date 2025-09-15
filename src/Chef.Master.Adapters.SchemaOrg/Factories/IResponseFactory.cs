using System.Threading.Tasks;
using Chef.Master.ViewModel;

namespace Chef.Master.Adapters.SchemaOrg.Factories
{
    public interface IResponseFactory
    {
        Task<RecipeViewModel> ForAsync(Schema.NET.Recipe recipe);
    }
}