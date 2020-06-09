using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Models.Forum;
using BBDProject.Clients.Repositories;
using BBDProject.Clients.Repositories.Forum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BBDProject.Clients.Services.Forum
{
    public class ForumService : BaseService, IForumService
    {
        private readonly IForumRepository _forumRepository;

        public ForumService(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        #region Topic

        [Authorize("User")]
        public async Task<ForumTopicViewModel> CreateTopic(ForumTopicForm form)
        {
            var topicId = await _forumRepository.CreateTopic(form);
            return await _forumRepository.GetTopic(topicId);
        }

        public async Task<ForumTopicForm> UpdateTopic(int topicId)
        {
            return Mapper.Map<ForumTopicForm>(await _forumRepository.GetTopic(topicId));
        }

        public async Task<ForumTopicViewModel> UpdateTopic(ForumTopicForm form)
        {
            var topicId = await _forumRepository.UpdateTopic(form);
            return await _forumRepository.GetTopic(topicId);
        }

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

        #endregion

        #region Post



        #endregion
    }
}
