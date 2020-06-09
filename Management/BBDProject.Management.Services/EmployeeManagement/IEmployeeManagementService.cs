using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Management.Models.Models.Employee;
using BBDProject.Shared.Models.User;

namespace BBDProject.Management.Services.EmployeeManagement
{
    public interface IEmployeeManagementService
    {
        Task<List<EmployeeModel>> GetAllEmployees();
        Task BanEmployee(int employeeId);
        Task UnbanEmployee(int employeeId);
    }
}
