using AutoMapper;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Models.Chat;
using BBDProject.Clients.Models.Forum;
using BBDProject.Clients.Models.Product;
using BBDProject.Management.Db.Dao;
using BBDProject.Management.Models.Models.Employee;
using BBDProject.Shared.Models.User;

namespace BBDProject.Management.WebApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region User
            CreateMap<DaoEmployee, EmployeeModel>().ReverseMap();
            CreateMap<DaoUser, UserModel>().ReverseMap();
            #endregion

            #region Chat
            CreateMap<DaoMessage, MessageModel>().ReverseMap();
            #endregion

            #region Product
            CreateMap<DaoProduct, ProductForm>().ReverseMap();
            CreateMap<DaoProduct, ProductViewModel>().ReverseMap();
            #endregion

            #region Forum
            CreateMap<DaoForumTopic, ForumTopicViewModel>().ReverseMap();
            CreateMap<ForumTopicViewModel, ForumTopicPreview>().ReverseMap();
            CreateMap<DaoForumTopic, ForumTopicForm>().ReverseMap();

            CreateMap<DaoForumPost, ForumPostViewModel>().ReverseMap();
            CreateMap<DaoForumPost, ForumPostForm>().ReverseMap();
            #endregion
        }
    }
}
