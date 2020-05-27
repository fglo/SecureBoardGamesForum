using System.ComponentModel.DataAnnotations;

namespace BBDProject.Shared.Models.User
{
    /// <summary>
    /// User Register Form
    /// </summary>
    public class UserRegisterForm
    {
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public string RegisterErrorMessage { get; set; }
    }
}
