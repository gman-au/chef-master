using System;

namespace Recipe.Formatter.Interfaces
{
    public interface ISchemaGenerator
    {
        string Generate(Type schemaType);
    }
}