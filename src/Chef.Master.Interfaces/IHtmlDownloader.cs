using System.Threading.Tasks;

namespace Chef.Master.Interfaces
{
    public interface IHtmlDownloader
    {
        Task<string> DownloadAsync(string url);
    }
}