using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RecipeApp.Core.Models.Recipe
{
    public class NewRecipeModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Malzemeler (Enter ile ayırınız.)")]
        public string Ingredients { get; set; }

        [Required]
        [Display(Name = "Talimatlar (Enter ile ayırınız.)")]
        public string Directions { get; set; }

        [Display(Name = "Görsel")]
        public IFormFile ImageFile { get; set; }

        [Required]
        [Display(Name = "Kategori")]
        public string CategoryId { get; set; }

        [Required]
        [Display(Name = "Anasayfada yayınlansın mı?")]
        public bool OnHomepage { get; set; }

        [Required]
        [Display(Name = "Hazırlanma Süresi")]
        public int Time { get; set; }

        [Required]
        [Display(Name = "Zorluk (1-5)")]
        public int Difficulty { get; set; }

        [Required]
        [Display(Name = "Porsiyon")]
        public int Servings { get; set; }
    }
}