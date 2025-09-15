using Chef.Master.Adapters.Groq.Definition.Requests;
using Chef.Master.Adapters.Groq.Definition;

namespace Chef.Master.Adapters.Groq
{
    public interface IGroqRequestBuilder
    {
        GroqRequest Build(string html);
    }
}