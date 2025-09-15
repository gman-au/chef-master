using System.Collections.Generic;

namespace Chef.Master.Adapters.SchemaOrg.Factories
{
    public interface IInstructionsFactory
    {
        IEnumerable<string> Parse(Schema.NET.Recipe recipe);
    }
}