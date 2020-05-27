using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Repositories.User;
using BBDProject.Shared.Models.User;

namespace BBDProject.Management.Services.UserManagement
{
    public class UserManagementService : BaseService, IUserManagementService
    {
        private readonly IUserRepository _userRepository;

        public UserManagementService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<List<UserModel>> GetAllUsers()
        {
            return Mapper.Map<List<UserModel>>(await _userRepository.GetAllUsers());
        }
    }
}
