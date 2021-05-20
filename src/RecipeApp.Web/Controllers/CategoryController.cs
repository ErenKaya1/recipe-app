using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Core.Enum;
using RecipeApp.Core.Models.Category;
using RecipeApp.Service.Contracts;
using RecipeApp.Web.Controllers.Base;

namespace RecipeApp.Web.Controllers
{
    [Authorize(Roles = "moderator")]
    public class CategoryController : BaseAdminController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var response = _categoryService.GetAll();
            if (response.Success)
                return View(response.Data);

            return NotFound();
        }

        [HttpGet]
        public IActionResult New() => View();

        [HttpPost]
        public async Task<IActionResult> New(NewCategoryModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _categoryService.InsertCategoryAsync(model);
            if (response.Success)
            {
                TempData["CategoryCreated"] = "Kategori başarıyla kaydedildi.";
                return RedirectToAction(nameof(Index));
            }

            switch (response.ErrorType)
            {
                case ErrorType.DuplicatedCategoryName:
                    ViewData["NewCategoryError"] = "Bu kategori adı daha önce kullanılmıştır.";
                    break;
                default:
                    ViewData["NewCategoryError"] = "Kategori eklenemedi";
                    break;
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string categoryId)
        {
            var response = await _categoryService.GetByIdAsync(categoryId);
            if (response.Success)
            {
                var model = _mapper.Map<EditCategoryModel>(response.Data);
                return View(model);
            }

            TempData["CategoryNotFound"] = "Kategori bulunamadı.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryModel model)
        {
            var response = await _categoryService.EditAsync(model);
            if (response.Success)
                return Ok();

            var errorMessage = "";
            switch (response.ErrorType)
            {
                case ErrorType.EntityNotFound:
                    errorMessage = "Kategori bulunamadı";
                    break;
                case ErrorType.DuplicatedCategoryName:
                    errorMessage = "Bu kategori adı daha önce kullanılmıştır.";
                    break;
                default:
                    errorMessage = "Kategori düzenlenemedi.";
                    break;
            }

            return BadRequest(new { errorMessage });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string categoryId)
        {
            var response = await _categoryService.DeleteByIdAsync(categoryId);
            if (response.Success)
                return Ok();

            return NotFound();
        }
    }
}