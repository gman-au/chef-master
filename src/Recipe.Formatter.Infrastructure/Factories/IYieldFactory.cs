namespace Recipe.Formatter.Infrastructure.Factories
{
    public interface IYieldFactory
    {
        string Parse(Schema.NET.Recipe recipe);
    }
}