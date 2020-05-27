using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Shared.Models.User;

namespace BBDProject.Management.Services.UserManagement
{
    public interface IUserManagementService
    {
        Task<List<UserModel>> GetAllUsers();
    }
}
