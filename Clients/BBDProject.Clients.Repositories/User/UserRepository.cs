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
    }
}
