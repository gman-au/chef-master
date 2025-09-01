using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recipe.Formatter.Interfaces;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Host.Controllers
{
    public class HomeController(IEnumerable<IRecipeAdapter> recipeAdapters) : Controller
    {
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
                var response = new RecipeParseResponseViewModel();
                
                foreach (var recipeAdapter in recipeAdapters.OrderBy(o => o.Order))
                {
                    response =
                        await
                            recipeAdapter
                                .ProcessAsync(value);

                    if (!response.Success) continue;

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
