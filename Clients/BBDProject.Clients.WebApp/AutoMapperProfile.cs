using AutoMapper;
using BBDProject.Clients.Db.Dao;
using BBDProject.Shared.Models.Chat;
using BBDProject.Shared.Models.User;

namespace BBDProject.Clients.WebApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DaoUser, UserModel>().ReverseMap();
            CreateMap<DaoMessage, MessageModel>().ReverseMap();
        }
    }
}
