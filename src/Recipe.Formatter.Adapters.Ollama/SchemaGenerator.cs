using System;
using Recipe.Formatter.Interfaces;
using NJsonSchema;

namespace Recipe.Formatter.Adapters.Ollama
{
    public class SchemaGenerator : ISchemaGenerator
    {
        public string Generate(Type schemaType)
        {
            var schema =
                JsonSchema
                    .FromType(schemaType);

            var jsonString =
                schema
                    .ToJson();

            return jsonString;
        }
    }
}