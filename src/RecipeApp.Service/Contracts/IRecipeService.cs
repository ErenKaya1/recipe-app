using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Core.Models.Recipe;
using RecipeApp.Entity.Entities;

namespace RecipeApp.Service.Contracts
{
    public interface IRecipeService
    {
        Task<ServiceResponse> InsertRecipeAsync(NewRecipeModel model);
        ServiceResponse<List<ListRecipeModel>> GetAll();
        ServiceResponse<List<ListRecipeModel>> GetAll(string categorySlug);
        Task<ServiceResponse<EditRecipeModel>> GetByIdAsync(string id);
        Task<ServiceResponse> EditAsync(EditRecipeModel model);
        Task<ServiceResponse> DeleteByIdAsync(string id);
        ServiceResponse<List<ListRecipeModel>> GetHomepageRecipes();
        Task<ServiceResponse<Recipe>> GetBySlugAsync(string slug);
        Task<ServiceResponse> DeleteRangeByCategoryId(string id);
    }
}