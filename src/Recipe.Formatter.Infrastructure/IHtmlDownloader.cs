using System.Threading.Tasks;

namespace Recipe.Formatter.Infrastructure
{
    public interface IHtmlDownloader
    {
        Task<string> DownloadAsync(string url);
    }
}