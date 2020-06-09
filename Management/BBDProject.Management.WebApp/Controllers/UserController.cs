using System.Net;
using System.Threading.Tasks;
using BBDProject.Management.Db.Dao;
using BBDProject.Management.Models.Exceptions;
using BBDProject.Management.Services.Employee;
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Login", userModel);
                }

                await _employeeService.LoginUser(userModel);
                return RedirectToAction("Index");
            }
            catch (ServiceException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
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