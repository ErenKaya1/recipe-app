using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RecipeApp.Service.Contracts
{
    public interface IImageService
    {
        Task SaveRecipeImage(IFormFile imageFile, string suffix);
        Task SaveRecipeImage(IFormFile imageFile, string suffix, string oldImageName);
        Task SaveCategoryImage(IFormFile imageFile, string suffix);
        Task SaveCategoryImage(IFormFile imageFile, string suffix, string oldImageName);
        void DeleteRecipeImage(string ImageName);
        void DeleteCategoryImage(string ImageName);
    }
}