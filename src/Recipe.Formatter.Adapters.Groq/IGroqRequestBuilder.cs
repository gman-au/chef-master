using Recipe.Formatter.Adapters.Groq.Definition;
using Recipe.Formatter.Adapters.Groq.Definition.Requests;

namespace Recipe.Formatter.Adapters.Groq
{
    public interface IGroqRequestBuilder
    {
        GroqRequest Build(string html);
    }
}