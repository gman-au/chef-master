using System.Collections.Generic;

namespace Recipe.Formatter.ViewModel
{
    public class RecipeViewModel
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public TimesViewModel Times { get; set; }

        public string Yield { get; set; }

        public IEnumerable<string> Ingredients { get; set; }

        public IEnumerable<string> Instructions { get; set; }
    }
}