using System.Text.Json.Serialization;

namespace Recipe.Formatter.Adapters.Ollama
{
    public class RecipeFormatSchema
    {
        [JsonPropertyName("schema")] public dynamic Schema { get; set; }
    }
}