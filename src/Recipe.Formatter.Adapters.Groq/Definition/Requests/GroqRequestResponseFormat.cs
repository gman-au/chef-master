using System.Text.Json.Serialization;

namespace Recipe.Formatter.Adapters.Groq.Definition.Requests
{
    public class GroqRequestResponseFormat
    {
        [JsonPropertyName("type")] public string Type { get; set; }

        [JsonPropertyName("json_schema")] public GroqJsonSchema JsonSchema { get; set; }
    }
}