using Recipe.Formatter.Adapters.Ollama.Definition;

namespace Recipe.Formatter.Adapters.Ollama
{
    public interface IOllamaRequestBuilder
    {
        OllamaRequest Build(string html);
    }
}