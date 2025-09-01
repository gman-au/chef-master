using System;
using System.Threading.Tasks;
using Recipe.Formatter.Adapters.SchemaOrg.Factories;
using Recipe.Formatter.Infrastructure;
using Recipe.Formatter.Interfaces;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Adapters.SchemaOrg
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

        public async Task<RecipeParseResponseViewModel> ProcessAsync(RecipeParseRequestViewModel request)
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

                response.Recipe =
                    await
                        _responseFactory
                            .ForAsync(thing);

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