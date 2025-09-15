using System.Text.Json.Serialization;

namespace Chef.Master.Adapters.Groq.Definition.Requests
{
    public class GroqJsonSchema
    {
        [JsonPropertyName("name")] public string Name  { get; set; }

        [JsonPropertyName("schema")] public dynamic Schema { get; set; }
    }
}