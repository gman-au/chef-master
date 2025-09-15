using Chef.Master.ViewModel;

namespace Chef.Master.Adapters.SchemaOrg.Factories
{
    public interface ITimesFactory
    {
        TimesViewModel Parse(Schema.NET.Recipe recipe);
    }
}