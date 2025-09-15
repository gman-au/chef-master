using System;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Chef.Master.Adapters.Groq.Definition.Requests;
using Chef.Master.Adapters.Groq.Definition;
using Chef.Master.Interfaces;
using Chef.Master.ViewModel;

namespace Chef.Master.Adapters.Groq
{
    public class GroqRequestBuilder(ISchemaGenerator schemaGenerator) : IGroqRequestBuilder
    {
        // private const string ModelName = "llama-3.1-8b-instant";
        private const string ModelName = "openai/gpt-oss-120b";

        public GroqRequest Build(string html)
        {
            var escapedHtml = html;

            var schemaString =
                schemaGenerator
                    .Generate(typeof(RecipeViewModel));

            var schemaNode =
                JsonNode
                    .Parse(schemaString);

            var prompt = new StringBuilder();

            prompt
                .Append("Extract recipe information from this HTML and return valid JSON matching the schema.")
                .AppendLine()
                .AppendLine("Webpage content:")
                .Append(escapedHtml);

            return new GroqRequest
            {
                Model = ModelName,
                Messages =
                [
                    new GroqMessage
                    {
                        Role = "user",
                        Content = prompt.ToString()
                    }
                ],
                ResponseFormat = new GroqRequestResponseFormat
                {
                    Type = "json_schema",
                    JsonSchema = new GroqJsonSchema
                    {
                        Name = "my_json_schema",
                        Schema = schemaNode
                    }
                },
                MaxTokens = 4096
            };
        }
    }
}