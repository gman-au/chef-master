using System.Collections.Generic;
using System.Linq;
using Schema.NET;

namespace Recipe.Formatter.Infrastructure.Factories
{
    public class InstructionsFactory : IInstructionsFactory
    {
        public IEnumerable<string> Parse(Schema.NET.Recipe recipe)
        {
            var values = recipe.RecipeInstructions;

            var howTos =
                values
                    .OfType<HowToStep>()
                    .ToList();

            if (howTos.Any())
                return howTos.Select(o => o.Text.FirstOrDefault());

            var strings =
                values
                    .OfType<string>()
                    .ToList();

            if (strings.Any())
                return strings;

            return new string[] { };
        }
    }
}