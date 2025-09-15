using System.Threading.Tasks;

namespace Chef.Master.Interfaces
{
    public interface IJsonParser
    {
        Task<Schema.NET.Recipe> ParseAsync(string json);
    }
}