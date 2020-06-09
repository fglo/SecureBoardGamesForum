using BBDProject.Clients.Models.Forum;
using BBDProject.Clients.Repositories.Forum;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBDProject.Management.Services.Forum
{
    public class ForumService : BaseService, IForumService
    {
        private readonly IForumRepository _forumRepository;

        public ForumService(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        #region Topic

        public async Task<List<ForumTopicPreview>> GetTopicPreviews()
        {
            var topics = await _forumRepository.GetTopics();
            topics.ForEach(_ => _.Content = _.Content.Substring(0, 128) + "...");
            return Mapper.Map<List<ForumTopicPreview>>(topics);
        }

        public async Task<ForumTopicViewModel> GetTopic(int topicId)
        {
            var topic = await _forumRepository.GetTopic(topicId);
            if (topic == null)
            {
                Error("Nie znaleziono szukanego wątku!");
            }

            topic.ForumPosts = (await _forumRepository.GetPosts(topic.Id))
                .OrderBy(_ => _.DateModified ?? _.DateAdded)
                .ToList();
            return topic;
        }

        public async Task DeleteTopic(int topicId)
        {
            await _forumRepository.DeleteTopic(topicId);
        }

        #endregion

        #region Post

        public async Task<int> DeletePost(int postId)
        {
            var post = await _forumRepository.GetPost(postId);
            if (post != null)
            {
                await _forumRepository.DeletePost(postId);
                return post.ForumTopicId;
            }

            return 0;
        }

        #endregion
    }
}
