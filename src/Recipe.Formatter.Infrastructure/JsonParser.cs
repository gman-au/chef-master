using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Recipe.Formatter.Interfaces;
using Schema.NET;

namespace Recipe.Formatter.Infrastructure
{
    public class JsonParser : IJsonParser
    {
        private const string RecipeType = "Recipe";

        public async Task<Schema.NET.Recipe> ParseAsync(string json)
        {
            // Usage
            if (!TryGetRecipe(json, out var recipeObject)) return null;

            var result =
                SchemaSerializer
                    .DeserializeObject<Schema.NET.Recipe>(recipeObject.ToString());

            return result;
        }

        private static bool TryGetRecipe(string jsonString, out JObject recipe)
        {
            recipe = null;
            var jsonObject = 
                JObject
                    .Parse(jsonString);

            // Case 1: Check if root object is directly a Recipe
            if (IsRecipe(jsonObject))
            {
                recipe = jsonObject;
                return true;
            }

            // Case 2: Check if there's a @graph array
            if (jsonObject["@graph"] is JArray graphArray)
            {
                foreach (var item in graphArray)
                {
                    var jObject = item as JObject;
                    if (jObject != null && IsRecipe(jObject))
                    {
                        // Return first Recipe found
                        recipe = jObject;
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsRecipe(JObject jObject)
        {
            var typeToken = jObject["@type"];

            if (typeToken is JArray typeArray)
            {
                return
                    typeArray
                        .Any(t => t.ToString() == RecipeType);
            }

            return typeToken?.ToString() == RecipeType;
        }
    }
}