using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RecipeApp.Core.Models.Recipe
{
    public class EditRecipeModel
    {
        public string Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Malzemeler (Enter ile ayırınız.)")]
        public string Ingredients { get; set; }

        [Display(Name = "Talimatlar (Enter ile ayırınız.)")]
        public string Directions { get; set; }

        [Display(Name = "Görsel")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Kategori")]
        public string CategoryId { get; set; }

        [Display(Name = "Anasayfada yayınlansın mı?")]
        public bool OnHomepage { get; set; }

        [Display(Name = "Hazırlanma Süresi")]
        public int Time { get; set; }

        [Display(Name = "Zorluk (1-5)")]
        public int Difficulty { get; set; }

        [Display(Name = "Porsiyon")]
        public int Servings { get; set; }

        public string ImageName { get; set; }
    }
}