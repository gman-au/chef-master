using System.Text.Json.Serialization;

namespace Chef.Master.Adapters.Groq.Definition.Requests
{
    public class GroqMessage
    {
        [JsonPropertyName("role")] public string Role { get; set; }

        [JsonPropertyName("content")] public string Content { get; set; }
    }
}