using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Management.Db.Dao;
using Microsoft.EntityFrameworkCore;

namespace BBDProject.Management.Repositories.User
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public async Task<DaoEmployee> Get(int userId)
        {
            return await DbContext.Users.FirstOrDefaultAsync(_ => _.Id == userId);
        }

        public async Task<List<DaoEmployee>> GetAll()
        {
            return await DbContext.Users.ToListAsync();
        }
    }
}
