using System.Threading.Tasks;
using Chef.Master.ViewModel;

namespace Chef.Master.Adapters.SchemaOrg.Factories
{
    public class ResponseFactory : IResponseFactory
    {
        private readonly IImageFactory _imageFactory;
        private readonly IInstructionsFactory _instructionsFactory;
        private readonly ITimesFactory _timesFactory;
        private readonly IYieldFactory _yieldFactory;

        public ResponseFactory(
            IInstructionsFactory instructionsFactory,
            IYieldFactory yieldFactory,
            IImageFactory imageFactory,
            ITimesFactory timesFactory)
        {
            _instructionsFactory = instructionsFactory;
            _yieldFactory = yieldFactory;
            _imageFactory = imageFactory;
            _timesFactory = timesFactory;
        }

        public async Task<RecipeViewModel> ForAsync(Schema.NET.Recipe recipe)
        {
            var steps =
                _instructionsFactory
                    .Parse(recipe);

            var image =
                _imageFactory
                    .Parse(recipe);

            var yield =
                _yieldFactory
                    .Parse(recipe);

            var times =
                _timesFactory
                    .Parse(recipe);

            return new RecipeViewModel
            {
                Title = recipe.Name,
                Yield = yield,
                ImageUrl = image,
                Ingredients = recipe.RecipeIngredient,
                Instructions = steps,
                Description = recipe.Description,
                Times = times
            };
        }
    }
}