using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Models.Forum;

namespace BBDProject.Management.Services.Forum
{
    public interface IForumService
    {
        #region Topic

        Task<List<ForumTopicPreview>> GetTopicPreviews();
        Task<ForumTopicViewModel> GetTopic(int topicId);
        Task DeleteTopic(int topicId);

        #endregion

        #region Post

        Task<int> DeletePost(int postId);

        #endregion
    }
}
