using System.Text;
using Chef.Master.Adapters.Ollama.Definition;
using Chef.Master.Interfaces;
using Chef.Master.ViewModel;

namespace Chef.Master.Adapters.Ollama
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