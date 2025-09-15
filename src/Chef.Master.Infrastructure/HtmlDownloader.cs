using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Chef.Master.Interfaces;

namespace Chef.Master.Infrastructure
{
    public class HtmlDownloader : IHtmlDownloader
    {
        public async Task<string> DownloadAsync(string url)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 |
                                                       SecurityProtocolType.Tls12;
                ServicePointManager.Expect100Continue = false;

                var client = new HttpClient();

                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept",
                    "text/html,application/xhtml+xml,application/xml");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                    "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

                var response = await client.GetStringAsync(url);

                return response;
            }
            catch (HttpRequestException)
            {
                throw new Exception("I couldn't reach that web site.");
            }
        }
    }
}