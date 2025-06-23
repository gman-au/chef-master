using System.Threading.Tasks;

namespace Recipe.Formatter.Infrastructure
{
    public interface IJsonParser
    {
        Task<Schema.NET.Recipe> ParseAsync(string json);
    }
}