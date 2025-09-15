using System.Text.Json.Serialization;

namespace Chef.Master.Adapters.Groq.Definition.Requests
{
    public class GroqErrorResponse
    {
        [JsonPropertyName("error")] public GroqErrorMessage Error { get; set; }
    }
}