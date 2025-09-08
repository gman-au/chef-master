using System.Threading.Tasks;
using QRCoder;
using Recipe.Formatter.Interfaces;

namespace Recipe.Formatter.Infrastructure
{
    public class QrCodeGenerator : IQrCodeGenerator
    {
        public async Task<string> GenerateAsync(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            using var qrGenerator = new QRCodeGenerator();

            using var qrCodeData =
                qrGenerator
                    .CreateQrCode(
                        value,
                        QRCodeGenerator.ECCLevel.Q
                    );

            using var qrCode = new Base64QRCode(qrCodeData);

            var qrCodeImage =
                qrCode
                    .GetGraphic(20);

            return qrCodeImage;
        }
    }
}