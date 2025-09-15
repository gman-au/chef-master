namespace Chef.Master.Adapters.SchemaOrg.Factories
{
    public interface IImageFactory
    {
        string Parse(Schema.NET.Recipe recipe);
    }
}