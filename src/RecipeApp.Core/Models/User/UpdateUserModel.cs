using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Core.Models.User
{
    public class UpdateUserModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "E-Posta")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}