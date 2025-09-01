using System.Text;
using Recipe.Formatter.Adapters.Ollama.Definition;
using Recipe.Formatter.Interfaces;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Adapters.Ollama
{
    public class OllamaRequestBuilder : IOllamaRequestBuilder
    {
        private readonly ISchemaGenerator _schemaGenerator;

        public OllamaRequestBuilder(ISchemaGenerator schemaGenerator)
        {
            _schemaGenerator = schemaGenerator;
        }

        public OllamaRequest Build(string html)
        {
            var schemaString =
                _schemaGenerator
                    .Generate(typeof(RecipeViewModel));

            var prompt = new StringBuilder();

            prompt
                .Append("Extract recipe information and return ONLY valid JSON matching this schema:")
                .Append(schemaString)
                .AppendLine("Webpage content:")
                .Append(html);

            return new OllamaRequest
            {
                Model = "llama3.1:8b",
                Prompt = prompt.ToString(),
                Format = "json",
                Stream = false
            };
        }
    }
}