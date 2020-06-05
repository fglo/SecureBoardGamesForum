using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using Microsoft.EntityFrameworkCore;

namespace BBDProject.Clients.Repositories.Chat
{
    public class ChatRepository : BaseRepository, IChatRepository
    {
        public async Task<int> SendMessage(string message, int authorId)
        {
            var dao = new DaoMessage()
            {
                Content = message,
                AuthorId = authorId,
                DateAdded = DateTime.Now
            };
            var entity = await DbContext.AddAsync(dao);
            await DbContext.SaveChangesAsync();
            return entity.Entity.Id;
        }

        public async Task<List<DaoMessage>> GetNewMessages(int userId, int lastMessageId)
        {
            List<DaoMessage> messages;
            if (lastMessageId > 0)
            {
                messages = await DbContext.Messages
                    .Include(m => m.Author)
                    .Where(m => m.Id > lastMessageId)
                    .OrderBy(m => m.DateAdded)
                    .ToListAsync();
            }
            else
            {
                messages = await DbContext.Messages
                    .Include(m => m.Author)
                    .OrderBy(m => m.DateAdded)
                    .ToListAsync();
            }

            return messages;
        }

        public async Task<List<DaoMessage>> GetPaged(int messagesPerPage, int pageNumber)
        {
            var messages = DbContext.Messages
                .Include(m => m.Author)
                .OrderByDescending(m => m.DateAdded)
                .Skip(messagesPerPage * (pageNumber - 1))
                .Take(messagesPerPage)
                .OrderBy(m => m.DateAdded);

            return await messages.ToListAsync();
        }
    }
}
