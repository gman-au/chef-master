using System.Threading.Tasks;

namespace Chef.Master.Interfaces
{
    public interface IJsonStripper
    {
        Task<string> StripFromHtmlAsync(string html);
    }
}