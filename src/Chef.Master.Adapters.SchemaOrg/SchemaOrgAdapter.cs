using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chef.Master.Adapters.SchemaOrg.Factories;
using Chef.Master.Interfaces;
using Chef.Master.ViewModel;

namespace Chef.Master.Adapters.SchemaOrg
{
    public class SchemaOrgAdapter : IRecipeAdapter
    {
        private readonly IResponseFormatter _formatter;
        private readonly IHtmlDownloader _htmlDownloader;
        private readonly IJsonParser _jsonParser;
        private readonly IJsonStripper _jsonStripper;
        private readonly IResponseFactory _responseFactory;

        public SchemaOrgAdapter(
            IHtmlDownloader htmlDownloader,
            IJsonStripper jsonStripper,
            IJsonParser jsonParser,
            IResponseFactory responseFactory,
            IResponseFormatter formatter
        )
        {
            _htmlDownloader = htmlDownloader;
            _jsonStripper = jsonStripper;
            _jsonParser = jsonParser;
            _responseFactory = responseFactory;
            _formatter = formatter;
        }

        public AdapterMetadataViewModel Metadata { get; set; } = new()
        {
            Name = "Schema.org",
            Url = "https://schema.org/",
            Ai = false,
            Index = 1,
            ConfirmPrompt = null
        };

        public async Task<RecipeParseResponseViewModel> ProcessAsync(
            RecipeParseRequestViewModel request,
            CancellationToken cancellationToken = default)
        {
            var response = new RecipeParseResponseViewModel
            {
                Success = true,
                Status = new StatusViewModel
                {
                    Stages = new StagesViewModel
                    {
                        CanConnect = false,
                        CanConvert = false,
                        CanFind = false,
                        CanInterpret = false
                    },
                    Url = request.Url,
                    Message = string.Empty
                }
            };

            try
            {
                var html =
                    await
                        _htmlDownloader
                            .DownloadAsync(request.Url);

                response.Status.Stages.CanConnect = true;

                var json =
                    await
                        _jsonStripper
                            .StripFromHtmlAsync(html);

                response.Status.Stages.CanFind = true;

                var thing =
                    await
                        _jsonParser
                            .ParseAsync(json);

                response.Status.Stages.CanInterpret = true;

                var recipe =
                    await
                        _responseFactory
                            .ForAsync(thing);

                if (!(recipe?.Ingredients ?? []).Any() || !(recipe?.Instructions ?? []).Any())
                    throw new Exception("No ingredients or instructions found");

                response.Recipe = recipe;

                response.Status.Stages.CanConvert = true;

                response =
                    _formatter
                        .For(response, request.CustomImageUrl);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Status.Message = ex.Message;
            }

            return response;
        }
    }
}