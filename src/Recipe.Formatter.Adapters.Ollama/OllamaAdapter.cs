using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Recipe.Formatter.Interfaces;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Adapters.Ollama
{
    public class OllamaAdapter : IRecipeAdapter
    {
        private readonly IHtmlDownloader _htmlDownloader;
        private readonly IHtmlCleaner _htmlCleaner;
        private readonly ILogger<OllamaAdapter> _logger;
        private readonly IOllamaRequestBuilder _ollamaRequestBuilder;

        private readonly JsonSerializerOptions _serializerOptions =
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        public OllamaAdapter(
            ILogger<OllamaAdapter> logger,
            IOllamaRequestBuilder ollamaRequestBuilder,
            IHtmlDownloader htmlDownloader,
            IHtmlCleaner htmlCleaner)
        {
            _logger = logger;
            _ollamaRequestBuilder = ollamaRequestBuilder;
            _htmlDownloader = htmlDownloader;
            _htmlCleaner = htmlCleaner;
        }

        public int Index { get; set; } = 3;

        public string ConfirmPrompt { get; set; } = "Would you like a Large Language Model (LLM) to attempt to interpret the recipe?";

        public async Task<RecipeParseResponseViewModel> ProcessAsync(
            RecipeParseRequestViewModel request,
            CancellationToken cancellationToken = default)
        {
            _logger
                .LogInformation("Sending Ollama API request");

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

                html =
                    _htmlCleaner
                        .Clean(html);

                response.Status.Stages.CanConnect = true;

                using var httpClient = new HttpClient();

                var ollamaEndpoint =
                    Environment.GetEnvironmentVariable("OLLAMA_ENDPOINT") ??
                    throw new Exception("Ollama API endpoint not defined");

                httpClient.BaseAddress = new Uri(ollamaEndpoint);

                var ollamaRequest =
                    _ollamaRequestBuilder
                        .Build(html);

                var jsonString =
                    JsonSerializer
                        .Serialize(ollamaRequest, _serializerOptions);

                _logger
                    .LogDebug("Ollama model request: {jsonString}", jsonString);

                var stopwatch = new Stopwatch();

                stopwatch
                    .Start();

                var ollamaResponse =
                    await
                        httpClient
                            .PostAsync(
                                "api/generate",
                                new StringContent(jsonString, Encoding.Default, "application/json"),
                                cancellationToken
                            );

                ollamaResponse
                    .EnsureSuccessStatusCode();

                stopwatch
                    .Stop();

                _logger
                    .LogInformation("Ollama HTTP response OK");
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