using System.Text.Json.Serialization;

namespace Chef.Master.Adapters.Groq.Definition.Requests
{
    public class GroqRequestResponseFormat
    {
        [JsonPropertyName("type")] public string Type { get; set; }

        [JsonPropertyName("json_schema")] public GroqJsonSchema JsonSchema { get; set; }
    }
}