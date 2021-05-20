using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RecipeApp.Core.Models.Category
{
    public class NewCategoryModel
    {
        [Required]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }

        [Display(Name = "Görsel")]
        public IFormFile ImageFile { get; set; }
    }
}