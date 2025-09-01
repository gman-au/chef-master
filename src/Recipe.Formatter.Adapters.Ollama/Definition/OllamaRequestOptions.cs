using System.Text.Json.Serialization;

namespace Recipe.Formatter.Adapters.Ollama.Definition
{
    public class OllamaRequestOptions
    {
        [JsonPropertyName("num_ctx")] public long? NumCtx { get; set; }
    }
}