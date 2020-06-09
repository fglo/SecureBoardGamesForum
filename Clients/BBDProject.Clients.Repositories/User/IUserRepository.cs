using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Shared.Models.User;

namespace BBDProject.Clients.Repositories.User
{
    public interface IUserRepository
    {
        Task<DaoUser> Get(int userId);
        Task<List<DaoUser>> GetAllUsers();
        Task SetLockoutEnabledAsync(int userId, bool enabled);
        Task SetLockoutEndDateAsync(int userId, DateTimeOffset? lockoutEnd);
        Task ResetAccessFailedCountAsync(int userId);
    }
}
