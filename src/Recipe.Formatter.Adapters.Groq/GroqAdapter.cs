using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Recipe.Formatter.Adapters.Groq.Definition.Requests;
using Recipe.Formatter.Adapters.Groq.Definition.Responses;
using Recipe.Formatter.Interfaces;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Adapters.Groq
{
    public class GroqAdapter : IRecipeAdapter
    {
        private readonly IGroqRequestBuilder _groqRequestBuilder;
        private readonly IHtmlCleaner _htmlCleaner;
        private readonly IHtmlDownloader _htmlDownloader;
        private readonly ILogger<GroqAdapter> _logger;
        private readonly IResponseFormatter _formatter;

        private readonly JsonSerializerOptions _serializerOptions =
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        public GroqAdapter(
            ILogger<GroqAdapter> logger,
            IGroqRequestBuilder groqRequestBuilder,
            IHtmlDownloader htmlDownloader,
            IHtmlCleaner htmlCleaner,
            IResponseFormatter formatter)
        {
            _logger = logger;
            _groqRequestBuilder = groqRequestBuilder;
            _htmlDownloader = htmlDownloader;
            _htmlCleaner = htmlCleaner;
            _formatter = formatter;
        }

        public AdapterMetadataViewModel Metadata { get; set; } = new()
        {
            Name = "Groq",
            Url = "https://groq.com/",
            Ai = true,
            Index = 2,
            ConfirmPrompt = "Would you like to use an AI (Groq) to attempt to interpret the recipe?"
        };

        public async Task<RecipeParseResponseViewModel> ProcessAsync(
            RecipeParseRequestViewModel request,
            CancellationToken cancellationToken = default)
        {
            _logger
                .LogInformation("Sending Groq API request");

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

                var groqEndpoint =
                    Environment.GetEnvironmentVariable("GROQ_ENDPOINT") ??
                    throw new Exception("Groq API endpoint not defined");

                httpClient.BaseAddress = new Uri(groqEndpoint);

                var groqApiKey =
                    Environment.GetEnvironmentVariable("GROQ_API_KEY") ??
                    throw new Exception("Groq API key not defined");

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", groqApiKey);

                var groqRequest =
                    _groqRequestBuilder
                        .Build(html);

                var jsonString =
                    JsonSerializer
                        .Serialize(groqRequest, _serializerOptions);

                _logger
                    .LogDebug("Groq model request: {jsonString}", jsonString);

                var stopwatch = new Stopwatch();

                stopwatch
                    .Start();

                var groqResponse =
                    await
                        httpClient
                            .PostAsync(
                                "openai/v1/chat/completions",
                                new StringContent(jsonString, Encoding.Default, "application/json"),
                                cancellationToken
                            );

                if (!groqResponse.IsSuccessStatusCode)
                {
                    var result =
                        await
                            groqResponse
                                .Content
                                .ReadFromJsonAsync<GroqErrorResponse>(cancellationToken);

                    throw new Exception(result.Error.Message);
                }

                groqResponse
                    .EnsureSuccessStatusCode();

                stopwatch
                    .Stop();

                _logger
                    .LogInformation("Groq HTTP response OK");

                var modelResponse =
                    await
                        groqResponse
                            .Content
                            .ReadFromJsonAsync<GroqResponse>(cancellationToken);

                _logger
                    .LogDebug("Groq model response: {jsonString}", JsonSerializer.Serialize(modelResponse));

                // Should be the first 'choice' in the response
                if (modelResponse?.Choices == null || modelResponse.Choices.Length == 0)
                    throw new Exception("No choices returned from Groq API");

                var recipeStringData =
                    modelResponse?.Choices[0]?.Message?.Content;

                if (string.IsNullOrWhiteSpace(recipeStringData))
                    throw new Exception("No recipe data returned from Groq API");

                var convertedResponse =
                    JsonSerializer
                        .Deserialize<RecipeViewModel>(recipeStringData);

                response.Recipe = convertedResponse;

                response.Status.Stages.CanConvert = true;

                response =
                    _formatter
                        .For(response, request.CustomImageUrl);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Status.Message = ex.Message;

                _logger
                    .LogError(ex, "Grok API returned error: {message}", ex.Message);
            }

            return response;
        }
    }
}