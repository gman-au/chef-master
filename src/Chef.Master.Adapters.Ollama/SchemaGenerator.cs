using System;
using Chef.Master.Interfaces;
using NJsonSchema;

namespace Chef.Master.Adapters.Ollama
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