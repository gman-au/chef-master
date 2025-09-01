using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Interfaces
{
    public interface IResponseFormatter
    {
        RecipeParseResponseViewModel For(RecipeParseResponseViewModel response, string customUrl);
    }
}