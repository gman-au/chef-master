namespace Recipe.Formatter.Infrastructure.Factories
{
    public interface IImageFactory
    {
        string Parse(Schema.NET.Recipe recipe);
    }
}