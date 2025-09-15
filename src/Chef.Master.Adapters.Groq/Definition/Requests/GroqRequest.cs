using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Chef.Master.Adapters.Groq.Definition.Requests
{
    public class GroqRequest
    {
        [JsonPropertyName("model")] public string Model { get; set; }

        [JsonPropertyName("messages")] public IEnumerable<GroqMessage> Messages { get; set; }

        [JsonPropertyName("response_format")] public GroqRequestResponseFormat ResponseFormat { get; set; }

        [JsonPropertyName("max_tokens")] public long MaxTokens { get; set; }
    }
}