using System.Text.Json.Serialization;

namespace Chef.Master.Adapters.Groq.Definition.Requests
{
    public class GroqErrorMessage
    {
        [JsonPropertyName("message")] public string Message { get; set; }

        [JsonPropertyName("type")] public string Type { get; set; }

        [JsonPropertyName("code")] public string Code { get; set; }
    }
}