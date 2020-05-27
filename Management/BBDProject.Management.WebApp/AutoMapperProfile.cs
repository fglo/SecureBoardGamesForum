using AutoMapper;
using BBDProject.Clients.Db.Dao;
using BBDProject.Management.Db.Dao;
using BBDProject.Management.Models.Models.Employee;
using BBDProject.Shared.Models.User;

namespace BBDProject.Management.WebApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DaoUser, UserModel>().ReverseMap();
            CreateMap<DaoEmployee, EmployeeModel>().ReverseMap();
        }
    }
}
