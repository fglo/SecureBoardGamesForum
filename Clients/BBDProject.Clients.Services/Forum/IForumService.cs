using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Models.Forum;

namespace BBDProject.Clients.Services.Forum
{
    public interface IForumService
    {
        #region Topic

        Task<ForumTopicViewModel> CreateTopic(ForumTopicForm form);
        Task<ForumTopicForm> UpdateTopic(int topicId);
        Task<ForumTopicViewModel> UpdateTopic(ForumTopicForm form);
        Task<List<ForumTopicPreview>> GetTopicPreviews();
        Task<ForumTopicViewModel> GetTopic(int topicId);

        #endregion

        #region Post



        #endregion
    }
}
