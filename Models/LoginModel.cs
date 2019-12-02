using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Полe \"Name\" є обов'язковим")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полe \"Password\" є обов'язковим")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Полe \"Name\" є обов'язковим")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полe \"Password\" є обов'язковим")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Полe \"ConfirmPassword\" є обов'язковим")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }

    }
}