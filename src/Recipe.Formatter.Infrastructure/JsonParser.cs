using System;
using System.Threading.Tasks;
using Recipe.Formatter.Interfaces;
using Schema.NET;

namespace Recipe.Formatter.Infrastructure
{
    public class JsonParser : IJsonParser
    {
        public async Task<Schema.NET.Recipe> ParseAsync(string json)
        {
            try
            {
                var result =
                    SchemaSerializer
                        .DeserializeObject<Schema.NET.Recipe>(json);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "I found some information in the site but I was unable to interpret it. Apologies.");
            }
        }
    }
}