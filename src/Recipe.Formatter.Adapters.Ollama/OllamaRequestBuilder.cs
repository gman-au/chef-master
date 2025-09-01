using System.Text;
using Recipe.Formatter.Adapters.Ollama.Definition;
using Recipe.Formatter.Interfaces;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Adapters.Ollama
{
    public class OllamaRequestBuilder(ISchemaGenerator schemaGenerator) : IOllamaRequestBuilder
    {
        private const string ModelName = "phi3:mini";

        public OllamaRequest Build(string html)
        {
            var schemaString =
                schemaGenerator
                    .Generate(typeof(RecipeViewModel));

            var prompt = new StringBuilder();

            prompt
                .Append("Extract recipe information and return ONLY valid JSON matching this schema:")
                .Append(schemaString)
                .AppendLine("Webpage content:")
                .Append(html);

            return new OllamaRequest
            {
                Model = ModelName,
                Prompt = prompt.ToString(),
                Format = "json",
                Stream = false,
                Options = new OllamaRequestOptions
                {
                    NumCtx = 65536
                }
            };
        }
    }
}