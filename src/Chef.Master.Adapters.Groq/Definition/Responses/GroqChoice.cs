using System.Text.Json.Serialization;

namespace Chef.Master.Adapters.Groq.Definition.Responses
{
    public class GroqChoice
    {
        [JsonPropertyName("index")] public int Index { get; set; }

        [JsonPropertyName("message")] public GroqMessage Message { get; set; }

        [JsonPropertyName("finish_reason")] public string FinishReason { get; set; }
    }
}