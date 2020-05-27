using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Management.Models.Models.Employee;
using BBDProject.Management.Repositories.User;
using BBDProject.Management.Services.UserManagement;
using BBDProject.Shared.Models.User;

namespace BBDProject.Management.Services.EmployeeManagement
{
    public class EmployeeManagementService : BaseService, IEmployeeManagementService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManagementService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
            return Mapper.Map<List<EmployeeModel>>(await _employeeRepository.GetAll());
        }
    }
}
