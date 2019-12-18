using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class LoginModel
    {
        

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "NameRequired")]
        [Display(Name="Name", ResourceType =typeof(Resources.Resource))]
        public string Name { get; set; }


        [Required(ErrorMessageResourceType= typeof(Resources.Resource),
            ErrorMessageResourceName ="PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Name="Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }
    }

    public class RegisterModel
    {

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "NameRequired")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), 
            ErrorMessageResourceName = "PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName ="ConfirmPasswordRequired")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordNotCoincided")]
        [Display(Name="ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        public string ConfirmPassword { get; set; }

    }
}