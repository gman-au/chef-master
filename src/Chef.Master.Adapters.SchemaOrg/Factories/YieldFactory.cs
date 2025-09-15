using Chef.Master.Infrastructure.Extensions;

namespace Chef.Master.Adapters.SchemaOrg.Factories
{
    public class YieldFactory : IYieldFactory
    {
        public string Parse(Schema.NET.Recipe recipe)
        {
            var yield = string.Empty;

            if (string.IsNullOrEmpty(yield))
                yield =
                    recipe
                        .Yield
                        .GetLongestValue();

            if (string.IsNullOrEmpty(yield))
                yield =
                    recipe
                        .RecipeYield
                        .GetLongestValue();

            return yield;
        }
    }
}