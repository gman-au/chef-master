using System.Collections.Generic;

namespace Recipe.Formatter.Infrastructure.Factories
{
    public interface IInstructionsFactory
    {
        IEnumerable<string> Parse(Schema.NET.Recipe recipe);
    }
}