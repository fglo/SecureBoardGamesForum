using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Management.Db.Dao;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BBDProject.Management.Repositories.User
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        private readonly UserManager<DaoEmployee> _userManager;

        public EmployeeRepository(UserManager<DaoEmployee> userManager)
        {
            _userManager = userManager;
        }

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
