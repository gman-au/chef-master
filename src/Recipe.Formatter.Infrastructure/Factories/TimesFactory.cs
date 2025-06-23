using System;
using System.Linq;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Infrastructure.Factories
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