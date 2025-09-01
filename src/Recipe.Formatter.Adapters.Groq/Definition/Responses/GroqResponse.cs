using System.Text.Json.Serialization;

namespace Recipe.Formatter.Adapters.Groq.Definition.Responses
{
    public class GroqResponse
    {
        [JsonPropertyName("id")] public string Id { get; set; }

        [JsonPropertyName("object")] public string Object { get; set; }

        [JsonPropertyName("created")] public long Created { get; set; }

        [JsonPropertyName("model")] public string Model { get; set; }

        [JsonPropertyName("choices")] public GroqChoice[] Choices { get; set; }

        [JsonPropertyName("usage")] public GroqUsage Usage { get; set; }
    }
}