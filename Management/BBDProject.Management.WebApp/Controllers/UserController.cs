using System.Net;
using System.Threading.Tasks;
using BBDProject.Management.Db.Dao;
using BBDProject.Management.Services.User;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BBDProject.Management.WebApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly SignInManager<DaoEmployee> _signInManager;
        private readonly IEmployeeService _employeeService;

        public UserController(UserManager<DaoEmployee> userManager,
            SignInManager<DaoEmployee> signInManager,
            IEmployeeService employeeService)
        {
            _signInManager = signInManager;
            _employeeService = employeeService;
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
            if (!ModelState.IsValid)
            {
                return View("Login", userModel);
            }

            var result = await _employeeService.LoginUser(userModel);
            if (result == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(result?.Key, result?.Value);
                return View("Login", userModel);
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