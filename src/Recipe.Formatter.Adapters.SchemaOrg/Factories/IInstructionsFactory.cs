using System.Collections.Generic;

namespace Recipe.Formatter.Adapters.SchemaOrg.Factories
{
    public interface IInstructionsFactory
    {
        IEnumerable<string> Parse(Schema.NET.Recipe recipe);
    }
}