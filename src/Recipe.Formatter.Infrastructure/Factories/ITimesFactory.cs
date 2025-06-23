using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Infrastructure.Factories
{
    public interface ITimesFactory
    {
        TimesViewModel Parse(Schema.NET.Recipe recipe);
    }
}