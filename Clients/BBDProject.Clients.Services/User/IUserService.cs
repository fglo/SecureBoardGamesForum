using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Models.User;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Clients.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="userLoginForm"></param>
        /// <returns></returns>
        Task LoginUser(UserLoginForm userLoginForm);
        /// <summary>
        /// Register new User
        /// </summary>
        /// <param name="userRegisterForm"></param>
        /// <returns></returns>
        Task<KeyValuePair<string, string>?> RegisterUser(UserRegisterForm userRegisterForm);
        Task<IdentityResult> ConfirmEmail(string username, string token);
        Task ResetPassword(ResetPasswordForm form);
        Task ResetPassword(int id);
        Task<IdentityResult> SetNewPassword(SetNewPasswordForm form);
    }
}
