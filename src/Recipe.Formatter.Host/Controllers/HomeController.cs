using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recipe.Formatter.Interfaces;
using Recipe.Formatter.ViewModel;

namespace Recipe.Formatter.Host.Controllers
{
    public class HomeController(
        IEnumerable<IRecipeAdapter> recipeAdapters,
        IQristAdapter qristAdapter = null
        ) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Reset()
        {
            return View("Index", null);
        }

        public async Task<IActionResult> Process(RecipeParseRequestViewModel value)
        {
            var view = value.Style ?? "Basic";

            string status = null;

            try
            {
                var response = new RecipeParseResponseViewModel();

                var lastModelIndex = 0;

                var allApplicableAdapters =
                    recipeAdapters
                        .Where(o => o.Metadata.Index > (value.LastModelIndex ?? -1))
                        .OrderBy(o => o.Metadata.Index);

                foreach (var recipeAdapter in allApplicableAdapters)
                {
                    lastModelIndex = recipeAdapter.Metadata.Index;

                    response =
                        await
                            recipeAdapter
                                .ProcessAsync(value);

                    if (response.Success)
                    {
                        ViewBag.PageTitle = $"{response.Recipe?.Title} - ({view})";

                        // apply QR code
                        if (qristAdapter != null)
                        {
                            var qrCode =
                                await
                                    qristAdapter
                                        .GenerateQristCodeAsync(
                                            response?.Recipe?.Title,
                                            response?.Recipe?.Ingredients ?? [],
                                            CancellationToken.None
                                        );

                            response.QrCodeBase64 = $"data:image/png;base64,{qrCode}";
                        }

                        return View(view, response);
                    }

                    // If next adapter message isn't empty, round trip back to user for confirmation
                    var nextAdapter =
                        recipeAdapters
                            .OrderBy(o => o.Metadata.Index)
                            .FirstOrDefault(o => o.Metadata.Index > lastModelIndex);

                    if (nextAdapter == null) continue;

                    var confirmationMessage =
                        nextAdapter?.Metadata?.ConfirmPrompt;

                    if (string.IsNullOrWhiteSpace(confirmationMessage)) continue;

                    response.Status.LastModelIndex = lastModelIndex;
                    response.Status.CustomImageUrl = value.CustomImageUrl;
                    response.Status.ConfirmationMessage = confirmationMessage;

                    return View("Index", response.Status);
                }

                return View("Index", response.Status);
            }
            catch (Exception ex)
            {
                return View("Index", new StatusViewModel { Message = status ?? ex.Message, Url = value.Url });
            }
        }
    }
}