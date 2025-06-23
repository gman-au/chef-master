using System.Threading.Tasks;

namespace Recipe.Formatter.Infrastructure
{
    public interface IJsonStripper
    {
        Task<string> StripFromHtmlAsync(string html);
    }
}