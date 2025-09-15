using System.Linq;
using Chef.Master.Infrastructure.Extensions;
using Chef.Master.Interfaces;
using Chef.Master.ViewModel;

namespace Chef.Master.Infrastructure
{
    public class ResponseFormatter : IResponseFormatter
    {
        public RecipeParseResponseViewModel For(RecipeParseResponseViewModel response, string customUrl)
        {
            var newResponse = new RecipeParseResponseViewModel();

            var recipe = response?.Recipe;

            var times = new TimesViewModel
            {
                CookingMinutes = recipe?.Times?.CookingMinutes,
                PreparationMinutes = recipe?.Times?.PreparationMinutes,
                TotalMinutes = recipe?.Times?.TotalMinutes
            };

            var ingredients =
                recipe?
                    .Ingredients?
                    .Select(o => o.ToFormatted())
                    .ToList();

            var instructions =
                recipe?
                    .Instructions?
                    .Select(o => o.ToFormatted())
                    .ToList();

            var url = recipe?.ImageUrl;

            if (!string.IsNullOrEmpty(customUrl))
                url = customUrl;

            newResponse.Recipe = new RecipeViewModel
            {
                Title = recipe?.Title,
                ImageUrl = url,
                Description = recipe?.Description.ToFormatted(),
                Times = times,
                Yield = recipe?.Yield,
                Ingredients = ingredients,
                Instructions = instructions
            };

            newResponse.Status = new StatusViewModel
            {
                Message = response?.Status?.Message,
                Url = response?.Status?.Url,
                Stages = new StagesViewModel
                {
                    CanConnect = (response?.Status?.Stages?.CanConnect).GetValueOrDefault(),
                    CanFind = (response?.Status?.Stages?.CanFind).GetValueOrDefault(),
                    CanInterpret = (response?.Status?.Stages?.CanInterpret).GetValueOrDefault(),
                    CanConvert = (response?.Status?.Stages?.CanConvert).GetValueOrDefault()
                }
            };

            newResponse.Success =
                (response?.Success)
                .GetValueOrDefault();

            return newResponse;
        }
    }
}