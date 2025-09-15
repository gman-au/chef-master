using System;
using System.Linq;
using Chef.Master.ViewModel;

namespace Chef.Master.Adapters.SchemaOrg.Factories
{
    public class TimesFactory : ITimesFactory
    {
        public TimesViewModel Parse(Schema.NET.Recipe recipe)
        {
            var prepTime = recipe.PrepTime.FirstOrDefault();
            var cookingTime = recipe.CookTime.FirstOrDefault();
            var totalTime = recipe.TotalTime.FirstOrDefault();

            return new TimesViewModel
            {
                PreparationMinutes = For(prepTime),
                CookingMinutes = For(cookingTime),
                TotalMinutes = For(totalTime)
            };
        }

        private static string For(TimeSpan? timeSpan)
        {
            var value = timeSpan?.ToString(@"h\h\ mm\m");
            return value;
        }
    }
}