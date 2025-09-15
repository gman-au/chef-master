using System;

namespace Chef.Master.Interfaces
{
    public interface ISchemaGenerator
    {
        string Generate(Type schemaType);
    }
}