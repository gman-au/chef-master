using System.Threading.Tasks;

namespace Recipe.Formatter.Interfaces
{
    public interface IQrCodeGenerator
    {
        Task<string> GenerateAsync(string value);
    }
}