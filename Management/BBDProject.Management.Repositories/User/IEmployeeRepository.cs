using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Management.Db.Dao;

namespace BBDProject.Management.Repositories.User
{
    public interface IEmployeeRepository
    {
        Task<DaoEmployee> Get(int userId);
        Task<List<DaoEmployee>> GetAll();
    }
}
