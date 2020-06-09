using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Repositories.User;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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

        public async Task BanUser(int userId)
        {
            var user = await GetUserAndCheckForNull(userId);
            await _userRepository.SetLockoutEnabledAsync(userId, true);
            await _userRepository.SetLockoutEndDateAsync(userId, DateTimeOffset.MaxValue);
        }

        public async Task UnbanUser(int userId)
        {
            var user = await GetUserAndCheckForNull(userId);
            await _userRepository.SetLockoutEnabledAsync(userId, false);
            await _userRepository.ResetAccessFailedCountAsync(userId);
        }

        private async Task<DaoUser> GetUserAndCheckForNull(int id, string errorMessage = null)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
            {
                Error(errorMessage ?? "Nie znaleziono użytkownika.", statusCode: StatusCodes.Status404NotFound);
            }

            return user;
        }
    }
}
