using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Shared.Models.User;

namespace BBDProject.Management.Services.Employee
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="userLoginForm"></param>
        /// <returns></returns>
        Task LoginUser(UserLoginForm userLoginForm);
    }
}
