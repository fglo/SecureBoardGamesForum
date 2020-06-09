using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Models.Resources;
using BBDProject.Clients.Models.User;
using BBDProject.Clients.Repositories.User;
using BBDProject.Shared.Models.User;
using BBDProject.Shared.Utils.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Serilog;

namespace BBDProject.Clients.Services.User
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<DaoUser> _userManager;
        private readonly RoleManager<DaoRole> _roleManager;
        private readonly SignInManager<DaoUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<DaoUser> userManager,
            RoleManager<DaoRole> roleManager,
            SignInManager<DaoUser> signInManager,
            IEmailService emailService,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _userRepository = userRepository;
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
                    EmailConfirmed = false
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

                            string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(confirmationToken);
                            var tokenEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
                            await _emailService.SendConfirmAccountEmail(new CancellationToken(), tokenEncoded,
                                new BaseUserInfo()
                                {
                                    UserName = user.UserName,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    Email = user.Email
                                });
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

        public async Task<IdentityResult> ConfirmEmail(string username, string token)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username) ??
                           await _userManager.FindByEmailAsync(username);
                if (user == null)
                {
                    return IdentityResult.Failed(new IdentityError() { Description = "Nie znaleziono użytkownika" });
                }

                var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
                var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
                var result = await _userManager.ConfirmEmailAsync(user, tokenDecoded);

                return result;
            }
            catch (Exception e)
            {
                return IdentityResult.Failed(new IdentityError() { Description = e.Message + " " + e.InnerException?.Message });
            }
        }

        public async Task ResetPassword(ResetPasswordForm form)
        {
            try
            {
                var user = await GetUserAndCheckForNull(form.UserNameOrEmail, "Reset hasła nie powiódł się.");
                await GenerateResetPasswordTokenAndSendMail(user);
            }
            catch (Exception e)
            {
                Error("Reset hasła nie powiódł się.");
            }
        }

        public async Task ResetPassword(int id)
        {
            try
            {
                var user = await GetUserAndCheckForNull(id);
                await GenerateResetPasswordTokenAndSendMail(user);
            }
            catch (Exception e)
            {
                Error("Reset hasła nie powiódł się.");
            }
        }

        public async Task<IdentityResult> SetNewPassword(SetNewPasswordForm form)
        {
            try
            {
                var user = await GetUserAndCheckForNull(form.UserName, "Reset hasła nie powiódł się.");
                var tokenDecodedBytes = WebEncoders.Base64UrlDecode(form.Token);
                var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
                return await _userManager.ResetPasswordAsync(user, tokenDecoded, form.Password);
            }
            catch (Exception e)
            {
                return IdentityResult.Failed(new IdentityError() { Description = e.Message + " " + e.InnerException?.Message });
            }
        }


        private async Task<DaoUser> GetUserAndCheckForNull(string userNameOrEmail, string errorMessage = null)
        {
            var user = await _userManager.FindByNameAsync(userNameOrEmail) ??
                       await _userManager.FindByEmailAsync(userNameOrEmail);
            if (user == null)
            {
                Error(errorMessage ?? "Nie znaleziono użytkownika", statusCode: StatusCodes.Status404NotFound);
            }

            return user;
        }

        private async Task<DaoUser> GetUserAndCheckForNull(int id, string errorMessage = null)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
            {
                Error(errorMessage ?? "Nie znaleziono użytkownika", statusCode: StatusCodes.Status404NotFound);
            }

            return user;
        }

        private async Task GenerateResetPasswordTokenAndSendMail(DaoUser user)
        {
            string confirmationToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(confirmationToken);
            var tokenEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            await _emailService.SendResetPasswordEmail(new CancellationToken(), tokenEncoded,
                new BaseUserInfo()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });
        }
    }
}
