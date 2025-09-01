using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recipe.Formatter.Infrastructure;
using Recipe.Formatter.Interfaces;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Host.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeAdapter _recipeAdapter;

        public HomeController(IRecipeAdapter recipeAdapter)
        {
            _recipeAdapter = recipeAdapter;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Process(RecipeParseRequestViewModel value)
        {
            var view = value.Style ?? "Basic";

            string status = null;

            try
            {
                var response = await _recipeAdapter.ProcessAsync(value);

                if (response.Success)
                {
                    ViewBag.PageTitle = $"{response.Recipe?.Title} - ({view})";
                    return View(view, response);
                }

                return View("Index", response.Status);
            }
            catch (Exception ex)
            {
                return View("Index", new StatusViewModel {Message = status ?? ex.Message, Url = value.Url});
            }
        }
    }
}
