using System.Net;
using System.Threading.Tasks;
using BBDProject.Management.Db.Dao;
using BBDProject.Management.Services.EmployeeManagement;
using BBDProject.Management.Services.User;
using BBDProject.Management.Services.UserManagement;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BBDProject.Management.WebApp.Controllers
{
    public class EmployeeManagementController : BaseController
    {
        private readonly IEmployeeManagementService _employeeManagementService;

        public EmployeeManagementController(IEmployeeManagementService employeeManagementService)
        {
            _employeeManagementService = employeeManagementService;
        }


        /// <summary>
        /// Login Main Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Employees()
        {
            return View(await _employeeManagementService.GetAllEmployees());
        }
    }
}