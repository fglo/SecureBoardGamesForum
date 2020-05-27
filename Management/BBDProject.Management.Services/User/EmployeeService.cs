using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Management.Db.Dao;
using BBDProject.Management.Models.Resources;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace BBDProject.Management.Services.User
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

        public async Task<KeyValuePair<string, string>?> LoginUser(UserLoginForm userLoginForm)
        {
            var viewModel = new UserLoginForm()
            {
                UserName = userLoginForm.UserName
            };

            var user = await _userManager.FindByNameAsync(userLoginForm.UserName) ??
                            await _userManager.FindByEmailAsync(userLoginForm.UserName);
            if (user == null)
            {
                return new KeyValuePair<string, string>("UserLoginForm.Username", "Wrong username");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName,
            userLoginForm.Password, false, false);
            if (result.Succeeded)
            {
                UserContext.UserId = user.Id;

                return null;
            }
            else if (result.IsLockedOut)
            {
                return new KeyValuePair<string, string>("UserLoginForm.Username", "User is locked");
            }
            else if (result.IsNotAllowed)
            {
                return new KeyValuePair<string, string>("UserLoginForm.Username", "User is not allowed");
            }
            else
            {
                return new KeyValuePair<string, string>("UserLoginForm.Password", "Wrong password");
            }
        }
    }
}
