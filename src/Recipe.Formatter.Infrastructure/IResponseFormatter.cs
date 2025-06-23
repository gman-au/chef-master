using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Infrastructure
{
    public interface IResponseFormatter
    {
        RecipeParseResponseViewModel For(RecipeParseResponseViewModel response, string customUrl);
    }
}