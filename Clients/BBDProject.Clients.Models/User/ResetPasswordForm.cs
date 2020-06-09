using System.ComponentModel.DataAnnotations;

namespace BBDProject.Clients.Models.User
{
    public class ResetPasswordForm
    {
        [Display(Name = "Username or E-mail")]
        [Required]
        public string UserNameOrEmail { get; set; }
    }
}

