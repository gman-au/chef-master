using Chef.Master.ViewModel;

namespace Chef.Master.Interfaces
{
    public interface IResponseFormatter
    {
        RecipeParseResponseViewModel For(RecipeParseResponseViewModel response, string customUrl);
    }
}