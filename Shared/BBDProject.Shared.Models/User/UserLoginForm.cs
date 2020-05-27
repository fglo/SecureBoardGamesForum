using System.ComponentModel.DataAnnotations;

namespace BBDProject.Shared.Models.User
{
    /// <summary>
    /// User Login Form
    /// </summary>
    public class UserLoginForm
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
