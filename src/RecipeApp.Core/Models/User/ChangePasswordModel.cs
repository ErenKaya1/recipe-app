using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Core.Models.User
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Eski Parola")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [Display(Name = "Yeni Parola")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Yeni Parola Tekrar")]
        [DataType(DataType.Password)]
        public string NewPasswordConfirm { get; set; }
    }
}