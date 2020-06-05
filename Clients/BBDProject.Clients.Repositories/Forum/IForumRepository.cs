using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Models.Forum;
using BBDProject.Clients.Models.Product;

namespace BBDProject.Clients.Repositories.Forum
{
    public interface IForumRepository
    {
        #region Topic
        Task<int> CreateTopic(ForumTopicForm form);
        Task<int> UpdateTopic(ForumTopicForm form);
        Task DeleteTopic(int topicId);
        Task<ForumTopicViewModel> GetTopic(int topicId);
        Task<List<ForumTopicViewModel>> GetTopics();
        Task<List<ForumTopicViewModel>> GetTopics(int pageNumber, int topicsOnPage);
        #endregion

        #region Post
        Task<int> CreatePost(ForumPostForm form);
        Task<int> UpdatePost(ForumPostForm form);
        Task DeletePost(int postId);
        Task<ForumPostViewModel> GetPost(int postId);
        Task<List<ForumPostViewModel>> GetPosts(int topicId);
        Task<List<ForumPostViewModel>> GetPosts(int topicId, int pageNumber, int postsOnPage);
        #endregion
    }
}
