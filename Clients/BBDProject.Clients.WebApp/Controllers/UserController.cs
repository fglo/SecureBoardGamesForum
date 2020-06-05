using System.Net;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Models.Exceptions;
using BBDProject.Clients.Services.User;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BBDProject.Clients.WebApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly SignInManager<DaoUser> _signInManager;
        private readonly IUserService _userService;

        public UserController(UserManager<DaoUser> userManager,
            SignInManager<DaoUser> signInManager,
            IUserService userService)
        {
            _signInManager = signInManager;
            _userService = userService;
        }


        /// <summary>
        /// Login Main Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

            return View("Login");
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

            return View();
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Authorize(UserLoginForm userModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Login", userModel);
                }

                await _userService.LoginUser(userModel);
                return RedirectToAction("Index");
            }
            catch (ServiceException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Login", userModel);
            }
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterForm userModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", userModel);
            }

            var result = await _userService.RegisterUser(userModel);
            if (result == null)
            {
                ViewBag.Message = "Rejestracja powiodła się";
                return View("Success");
            }
            else
            {
                ModelState.AddModelError(result?.Key, result?.Value);
                return View("Register", userModel);
            }
        }

        /// <summary>
        /// Logout user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}