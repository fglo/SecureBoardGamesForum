using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Shared.Models.User;
using Microsoft.EntityFrameworkCore;

namespace BBDProject.Clients.Repositories.User
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task<DaoUser> Get(int userId)
        {
            return await DbContext.Users.FirstOrDefaultAsync(_ => _.Id == userId);
        }

        public async Task<List<DaoUser>> GetAllUsers()
        {
            return await DbContext.Users.ToListAsync();
        }

        public async Task SetLockoutEnabledAsync(int userId, bool enabled)
        {
            var dao = await DbContext.Users.FirstOrDefaultAsync(_ => _.Id == userId);
            dao.LockoutEnabled = enabled;
            await DbContext.SaveChangesAsync();
        }

        public async Task SetLockoutEndDateAsync(int userId, DateTimeOffset? lockoutEnd)
        {
            var dao = await DbContext.Users.FirstOrDefaultAsync(_ => _.Id == userId);
            dao.LockoutEnd = lockoutEnd;
            await DbContext.SaveChangesAsync();
        }

        public async Task ResetAccessFailedCountAsync(int userId)
        {
            var dao = await DbContext.Users.FirstOrDefaultAsync(_ => _.Id == userId);
            dao.AccessFailedCount = 0;
            await DbContext.SaveChangesAsync();
        }
    }
}
