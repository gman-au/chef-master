using System.Text.Json.Serialization;

namespace Chef.Master.Adapters.Ollama
{
    public class RecipeFormatSchema
    {
        [JsonPropertyName("schema")] public dynamic Schema { get; set; }
    }
}