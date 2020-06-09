using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Management.Db.Dao;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Management.Services.Employee
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly UserManager<DaoEmployee> _userManager;
        private readonly RoleManager<DaoRole> _roleManager;
        private readonly SignInManager<DaoEmployee> _signInManager;
        //private readonly IMapper _mapper;

        public EmployeeService(UserManager<DaoEmployee> userManager,
            RoleManager<DaoRole> roleManager,
            SignInManager<DaoEmployee> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task LoginUser(UserLoginForm userLoginForm)
        {
            var viewModel = new UserLoginForm()
            {
                UserName = userLoginForm.UserName
            };

            var user = await _userManager.FindByNameAsync(userLoginForm.UserName) ??
                       await _userManager.FindByEmailAsync(userLoginForm.UserName);
            if (user == null)
            {
                Error("Nieprawidłowa nazwa użytkownika lub hasło!");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName,
                userLoginForm.Password, false, false);
            if (result.Succeeded)
            {
                UserContext.UserId = user.Id;
            }
            else if (result.IsLockedOut)
            {
                Error("Uzytkownik został zablokowany!");
            }
            else if (result.IsNotAllowed)
            {
                Error("Uzytkownik jest nieaktywny!");
            }
            else
            {
                Error("Nieprawidłowa nazwa użytkownika lub hasło!");
            }
        }
    }
}
