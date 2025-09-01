using System.Text.Json.Serialization;

namespace Recipe.Formatter.Adapters.Groq.Definition.Requests
{
    public class GroqJsonSchema
    {
        [JsonPropertyName("name")] public string Name  { get; set; }

        [JsonPropertyName("schema")] public dynamic Schema { get; set; }
    }
}