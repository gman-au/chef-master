using System.Text.Json.Serialization;

namespace Recipe.Formatter.Adapters.Groq.Definition.Requests
{
    public class GroqErrorResponse
    {
        [JsonPropertyName("error")] public GroqErrorMessage Error { get; set; }
    }
}