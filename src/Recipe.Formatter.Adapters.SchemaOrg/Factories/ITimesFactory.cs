using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Adapters.SchemaOrg.Factories
{
    public interface ITimesFactory
    {
        TimesViewModel Parse(Schema.NET.Recipe recipe);
    }
}