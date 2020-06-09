using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Management.Db.Dao;
using BBDProject.Management.Models.Models.Employee;
using BBDProject.Management.Repositories.User;
using BBDProject.Management.Services.UserManagement;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Management.Services.EmployeeManagement
{
    public class EmployeeManagementService : BaseService, IEmployeeManagementService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<DaoEmployee> _userManager;

        public EmployeeManagementService(IEmployeeRepository employeeRepository,
            UserManager<DaoEmployee> userManager)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
        }

        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
            return Mapper.Map<List<EmployeeModel>>(await _employeeRepository.GetAll());
        }

        public async Task BanEmployee(int employeeId)
        {
            var user = await GetUserAndCheckForNull(employeeId);
            var result = await _userManager.SetLockoutEnabledAsync(user, true);
            if (result.Succeeded)
            {
                result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }
            else
            {
                Error("Deaktywacja użytkownika nie powiodła się.");
            }
        }

        public async Task UnbanEmployee(int employeeId)
        {
            var user = await GetUserAndCheckForNull(employeeId);
            var result = await _userManager.SetLockoutEnabledAsync(user, false);
            if (result.Succeeded)
            {
                result = await _userManager.ResetAccessFailedCountAsync(user);
            }
            else
            {
                Error("Aktywacja użytkownika nie powiodła się.");
            }
        }

        private async Task<DaoEmployee> GetUserAndCheckForNull(string userNameOrEmail, string errorMessage = null)
        {
            var user = await _userManager.FindByNameAsync(userNameOrEmail) ??
                       await _userManager.FindByEmailAsync(userNameOrEmail);
            if (user == null)
            {
                Error(errorMessage ?? "Nie znaleziono użytkownika.", statusCode: StatusCodes.Status404NotFound);
            }

            return user;
        }

        private async Task<DaoEmployee> GetUserAndCheckForNull(int id, string errorMessage = null)
        {
            var user = await _employeeRepository.Get(id);
            if (user == null)
            {
                Error(errorMessage ?? "Nie znaleziono użytkownika.", statusCode: StatusCodes.Status404NotFound);
            }

            return user;
        }
    }
}
