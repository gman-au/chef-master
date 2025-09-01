using System.Threading.Tasks;

namespace Recipe.Formatter.Interfaces
{
    public interface IJsonParser
    {
        Task<Schema.NET.Recipe> ParseAsync(string json);
    }
}