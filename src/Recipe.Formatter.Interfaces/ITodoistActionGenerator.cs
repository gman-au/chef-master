using System.Collections.Generic;

namespace Recipe.Formatter.Interfaces
{
    public interface ITodoistActionGenerator
    {
        string Generate(string recipeName, IEnumerable<string> ingredients);
    }
}