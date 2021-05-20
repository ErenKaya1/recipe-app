using Microsoft.AspNetCore.Http;

namespace RecipeApp.Core.Models.Category
{
    public class EditCategoryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
    }
}