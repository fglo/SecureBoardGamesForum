using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Models.Resources;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace BBDProject.Clients.Services.User
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<DaoUser> _userManager;
        private readonly RoleManager<DaoRole> _roleManager;
        private readonly SignInManager<DaoUser> _signInManager;
        //private readonly IMapper _mapper;

        public UserService(UserManager<DaoUser> userManager,
            RoleManager<DaoRole> roleManager,
            SignInManager<DaoUser> signInManager)
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

        public async Task<KeyValuePair<string, string>?> RegisterUser(UserRegisterForm userRegisterForm)
        {
            Log.Information("Rejestracja!");
            var viewModel = new UserLoginForm()
            {
                UserName = userRegisterForm.UserName
            };

            try
            {
                var user = _userManager.FindByNameAsync(userRegisterForm.UserName).Result;
                if (user != null)
                {
                    Log.Error("Username taken");
                    return new KeyValuePair<string, string>("UserRegisterForm.Username", "Username taken");
                }

                user = _userManager.FindByEmailAsync(userRegisterForm.Email).Result;
                if (user != null)
                {
                    Log.Error("Email taken");
                    return new KeyValuePair<string, string>("UserRegisterForm.Email", "Email taken");
                }

                var registerUser = new DaoUser()
                {
                    FirstName = userRegisterForm.FirstName,
                    LastName = userRegisterForm.LastName,
                    Email = userRegisterForm.Email,
                    UserName = userRegisterForm.UserName,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(registerUser, userRegisterForm.Password);
                if (result.Succeeded)
                {
                    var companyId = 0;
                    try
                    {
                        user = await _userManager.FindByNameAsync(userRegisterForm.UserName);
                        if (user != null)
                        {
                            await _userManager.AddToRoleAsync(registerUser, RoleNames.User);
                            viewModel = new UserLoginForm()
                            {
                                UserName = user.UserName
                            };
                            Log.Information("Registration successfully completed.");
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        await _userManager.DeleteAsync(user);
                        Log.Error("Registration failed.", e.Message);
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Registration failed.", e.Message);
                throw;
            }

            return Error<KeyValuePair<string, string>>("Registration failed.");
        }
    }
}
