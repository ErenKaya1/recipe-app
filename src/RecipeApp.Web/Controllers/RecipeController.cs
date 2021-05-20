using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Core.Models.Recipe;
using RecipeApp.Service.Contracts;
using RecipeApp.Web.Controllers.Base;

namespace RecipeApp.Web.Controllers
{
    [Authorize(Roles = "moderator")]
    public class RecipeController : BaseAdminController
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var response = _recipeService.GetAll();
            if (response.Success)
                return View(response.Data);

            return BadRequest();
        }

        [HttpGet]
        public IActionResult New() => View();

        [HttpPost]
        public async Task<IActionResult> New(NewRecipeModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _recipeService.InsertRecipeAsync(model);
            if (response.Success)
            {
                TempData["RecipeCreated"] = "Tarif başarıyla eklendi";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string recipeId)
        {
            var response = await _recipeService.GetByIdAsync(recipeId);
            if (response.Success)
                return View(response.Data);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditRecipeModel model)
        {
            var response = await _recipeService.EditAsync(model);
            if (response.Success)
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string recipeId)
        {
            var response = await _recipeService.DeleteByIdAsync(recipeId);
            if (response.Success)
                return Ok();

            return NotFound();
        }
    }
}