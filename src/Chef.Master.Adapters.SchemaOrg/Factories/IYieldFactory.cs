namespace Chef.Master.Adapters.SchemaOrg.Factories
{
    public interface IYieldFactory
    {
        string Parse(Schema.NET.Recipe recipe);
    }
}