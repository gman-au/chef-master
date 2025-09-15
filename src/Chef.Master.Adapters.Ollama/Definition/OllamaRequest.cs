using System.Text.Json.Serialization;

namespace Chef.Master.Adapters.Ollama.Definition
{
    public class OllamaRequest
    {
        [JsonPropertyName("model")] public string Model { get; set; }

        [JsonPropertyName("prompt")] public string Prompt { get; set; }

        [JsonPropertyName("format")] public string Format { get; set; }

        [JsonPropertyName("stream")] public bool Stream { get; set; }

        [JsonPropertyName("options")] public OllamaRequestOptions Options { get; set; }
    }
}