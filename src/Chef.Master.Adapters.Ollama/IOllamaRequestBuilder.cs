using Chef.Master.Adapters.Ollama.Definition;

namespace Chef.Master.Adapters.Ollama
{
    public interface IOllamaRequestBuilder
    {
        OllamaRequest Build(string html);
    }
}