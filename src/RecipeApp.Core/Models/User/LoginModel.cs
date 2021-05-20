using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Core.Models.User
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Beni Hatırla")]
        public bool IsPersistent { get; set; }
    }
}