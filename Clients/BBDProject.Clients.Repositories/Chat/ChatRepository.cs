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

        public async Task<List<DaoMessage>> GetAllMessages()
        {
            throw new NotImplementedException();
        }

        public async Task<List<DaoMessage>> GetLast()
        {
            throw new NotImplementedException();
        }

        public async Task<List<DaoMessage>> GetNotRead()
        {
            throw new NotImplementedException();
        }

        public async Task<List<DaoMessage>> GetPaged(int messagesPerPage, int pageNumber)
        {
            var messages = DbContext.Messages.OrderByDescending(m => m.DateAdded)
                .Skip(messagesPerPage * (pageNumber - 1))
                .Take(messagesPerPage)
                .OrderBy(m => m.DateAdded);

            return await messages.ToListAsync();
        }
    }
}
