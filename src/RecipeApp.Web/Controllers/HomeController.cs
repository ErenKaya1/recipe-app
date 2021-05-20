using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Service.Contracts;
using RecipeApp.Web.Controllers.Base;
using RecipeApp.Web.Models;

namespace RecipeApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRecipeService _recipeService;

        public HomeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/detaylar/{slug}")]
        public async Task<IActionResult> RecipeDetails(string slug)
        {
            var response = await _recipeService.GetBySlugAsync(slug);
            if (response.Success)
                return View(response.Data);

            return NotFound();
        }

        [HttpGet("/kategori/{slug}")]
        public IActionResult Recipes(string slug)
        {
            var response = _recipeService.GetAll(slug);
            if (response.Success)
                return View(response.Data);

            return NotFound();
        }

        [HttpGet("/tarifler")]
        public IActionResult AllRecipes()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
