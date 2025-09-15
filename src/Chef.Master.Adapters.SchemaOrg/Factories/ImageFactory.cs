using System;
using System.Linq;
using Schema.NET;

namespace Chef.Master.Adapters.SchemaOrg.Factories
{
    public class ImageFactory : IImageFactory
    {
        public string Parse(Schema.NET.Recipe recipe)
        {
            var values = recipe.Image;

            var imageObjects =
                values
                    .OfType<ImageObject>()
                    .ToList();

            if (imageObjects.Any())
            {
                var urls =
                    imageObjects
                        .SelectMany(o => o.Url)
                        .Select(o => o.AbsoluteUri);

                return
                    urls
                        .FirstOrDefault(o => !string.IsNullOrEmpty(o));
            }

            var uris =
                values
                    .OfType<Uri>()
                    .ToList();

            if (uris.Any())
            {
                var urls =
                    uris
                        .Select(o => o.AbsoluteUri);

                return
                    urls
                        .FirstOrDefault(o => !string.IsNullOrEmpty(o));
            }

            var strings =
                values
                    .OfType<string>()
                    .ToList();

            if (strings.Any())
                return strings.FirstOrDefault();

            return string.Empty;
        }
    }
}