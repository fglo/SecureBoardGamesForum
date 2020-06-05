using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Models.Forum;
using BBDProject.Clients.Models.Product;
using BBDProject.Shared.Utils.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BBDProject.Clients.Repositories.Forum
{
    public class ForumRepository : BaseRepository, IForumRepository
    {
        #region Topic

        public async Task<int> CreateTopic(ForumTopicForm form)
        {
            var dao = Mapper.Map<DaoForumTopic>(form);
            var entry = await DbContext.AddAsync(dao);
            await DbContext.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public async Task<int> UpdateTopic(ForumTopicForm form)
        {
            var dao = await DbContext.ForumTopics.AsNoTracking().FirstOrDefaultAsync(_ => _.Id == form.Id);
            if (dao == null || dao.Deleted)
            {
                Error("Wątek nie istnieje!");
            }
            dao = Mapper.Map<DaoForumTopic>(form);
            var entry = DbContext.Update(dao);
            await DbContext.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public async Task DeleteTopic(int topicId)
        {
            var dao = await DbContext.ForumTopics.FirstOrDefaultAsync(_ => _.Id == topicId);
            if (dao != null)
            {
                dao.Deleted = true;
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<ForumTopicViewModel> GetTopic(int topicId)
        {
            var dao = await DbContext.ForumTopics
                .Include(_ => _.Author)
                .FirstOrDefaultAsync(_ => _.Id == topicId);
            if (dao == null || dao.Deleted)
            {
                Error("Wątek nie istnieje!");
            }
            return Mapper.Map<ForumTopicViewModel>(dao);
        }

        public async Task<List<ForumTopicViewModel>> GetTopics()
        {
            var dao = await DbContext.ForumTopics
                .Where(_ => !_.Deleted)
                .Include(_ => _.Author)
                .OrderByDescending(_ => _.DateModified ?? _.DateAdded)
                .ToListAsync();
            return Mapper.Map<List<ForumTopicViewModel>>(dao);
        }

        public async Task<List<ForumTopicViewModel>> GetTopics(int pageNumber, int topicsOnPage)
        {
            var dao = await DbContext.ForumTopics
                .Where(_ => !_.Deleted)
                .Include(_ => _.Author)
                .OrderByDescending(_ => _.DateModified ?? _.DateAdded)
                .Skip((pageNumber - 1) * topicsOnPage)
                .Take(topicsOnPage)
                .ToListAsync();
            return Mapper.Map<List<ForumTopicViewModel>>(dao);
        }

        #endregion

        #region Post

        public async Task<int> CreatePost(ForumPostForm form)
        {
            var dao = Mapper.Map<DaoForumPost>(form);
            var entry = await DbContext.AddAsync(dao);
            await DbContext.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public async Task<int> UpdatePost(ForumPostForm form)
        {
            var dao = await DbContext.ForumPosts.AsNoTracking().FirstOrDefaultAsync(_ => _.Id == form.Id);
            if (dao == null || dao.Deleted)
            {
                Error("Post nie istnieje!");
            }
            dao = Mapper.Map<DaoForumPost>(form);
            var entry = DbContext.Update(dao);
            await DbContext.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public async Task DeletePost(int postId)
        {
            var dao = await DbContext.ForumPosts.FirstOrDefaultAsync(_ => _.Id == postId);
            if (dao != null)
            {
                dao.Deleted = true;
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<ForumPostViewModel> GetPost(int postId)
        {
            var dao = await DbContext.ForumPosts
                .Include(_ => _.Author)
                .FirstOrDefaultAsync(_ => _.Id == postId);
            if (dao == null || dao.Deleted)
            {
                Error("Post nie istnieje!");
            }
            return Mapper.Map<ForumPostViewModel>(dao);
        }

        public async Task<List<ForumPostViewModel>> GetPosts(int topicId)
        {
            var dao = await DbContext.ForumPosts
                .Where(_ => !_.Deleted)
                .Include(_ => _.Author)
                .OrderBy(_ => _.DateAdded)
                .ToListAsync();
            return Mapper.Map<List<ForumPostViewModel>>(dao);
        }

        public async Task<List<ForumPostViewModel>> GetPosts(int topicId, int pageNumber, int postsOnPage)
        {
            var dao = await DbContext.ForumPosts
                .Where(_ => !_.Deleted)
                .Include(_ => _.Author)
                .OrderBy(_ => _.DateAdded)
                .Skip((pageNumber - 1) * postsOnPage)
                .Take(postsOnPage)
                .ToListAsync();
            return Mapper.Map<List<ForumPostViewModel>>(dao);
        }

        #endregion
    }
}
