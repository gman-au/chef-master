using System.Threading.Tasks;

namespace Recipe.Formatter.Interfaces
{
    public interface IHtmlDownloader
    {
        Task<string> DownloadAsync(string url);
    }
}