using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Recipe.Formatter.Interfaces;

namespace Recipe.Formatter.Infrastructure
{
    public class QristAdapter(ILogger<QristAdapter> logger) : IQristAdapter
    {
        private const string TodoistProvider = "Todoist";
        private const string DefaultLabel = "Shopping";

        private readonly JsonSerializerOptions _serializerOptions =
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        public async Task<string> GenerateQristCodeAsync(
            string recipeName,
            IEnumerable<string> ingredients,
            CancellationToken cancellationToken
        )
        {
            if (!(ingredients ?? []).Any())
                return null;

            var qristIngredients =
                ingredients
                    .Select(o => new TodoistTask
                    {
                        Content = o,
                        Labels =
                        [
                            DefaultLabel
                        ]
                    });

            var qristRequest =
                new QristRequest
                {
                    Provider = TodoistProvider,
                    Data = new CreateTodoistTaskRequest
                    {
                        Tasks = qristIngredients
                    }
                };

            using var httpClient = new HttpClient();

            var qristEndpoint =
                Environment
                    .GetEnvironmentVariable("QRIST_ENDPOINT")
                ?? throw new Exception("QRIST_ENDPOINT environment variable not set.");

            httpClient.BaseAddress = new Uri(qristEndpoint);

            var jsonString =
                JsonSerializer
                    .Serialize(qristRequest, _serializerOptions);

            logger
                .LogDebug("Qrist request: {jsonString}", jsonString);

            var stopwatch = new Stopwatch();

            stopwatch
                .Start();

            var qristResponse =
                await
                    httpClient
                        .PostAsync(
                            "api/build/code",
                            new StringContent(jsonString, Encoding.Default, "application/json"),
                            cancellationToken
                        );

            qristResponse
                .EnsureSuccessStatusCode();

            var qrCodeBase64 =
                await
                    qristResponse
                        .Content
                        .ReadAsStringAsync(cancellationToken);

            stopwatch
                .Stop();

            logger
                .LogInformation("Qrist HTTP response OK");

            return qrCodeBase64;
        }

        private class QristRequest
        {
            [JsonPropertyName("provider")] public string Provider { get; set; }

            [JsonPropertyName("data")] public CreateTodoistTaskRequest Data { get; set; }
        }

        private class CreateTodoistTaskRequest
        {
            [JsonPropertyName("tasks")] public IEnumerable<TodoistTask> Tasks { get; set; }
        }


        private class TodoistTask
        {
            [JsonPropertyName("content")] public string Content { get; set; }

            [JsonPropertyName("description")] public string Description { get; set; }

            [JsonPropertyName("labels")] public string[] Labels { get; set; }

            [JsonPropertyName("priority")] public int Priority { get; set; }
        }
    }
}