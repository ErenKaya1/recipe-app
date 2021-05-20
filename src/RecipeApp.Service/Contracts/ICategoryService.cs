using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Core.Models.Category;
using RecipeApp.Entity.Entities;

namespace RecipeApp.Service.Contracts
{
    public interface ICategoryService
    {
        ServiceResponse<List<Category>> GetAll();
        Task<ServiceResponse> InsertCategoryAsync(NewCategoryModel model);
        Task<ServiceResponse<Category>> GetByIdAsync(string id);
        Task<ServiceResponse> EditAsync(EditCategoryModel model);
        Task<ServiceResponse> DeleteByIdAsync(string id);
    }
}