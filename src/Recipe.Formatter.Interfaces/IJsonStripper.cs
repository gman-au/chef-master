using System.Threading.Tasks;

namespace Recipe.Formatter.Interfaces
{
    public interface IJsonStripper
    {
        Task<string> StripFromHtmlAsync(string html);
    }
}