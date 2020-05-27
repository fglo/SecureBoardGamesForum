using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Shared.Models.User;

namespace BBDProject.Clients.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="userLoginForm"></param>
        /// <returns></returns>
        Task<KeyValuePair<string, string>?> LoginUser(UserLoginForm userLoginForm);
        /// <summary>
        /// Register new User
        /// </summary>
        /// <param name="userRegisterForm"></param>
        /// <returns></returns>
        Task<KeyValuePair<string, string>?> RegisterUser(UserRegisterForm userRegisterForm);
    }
}
