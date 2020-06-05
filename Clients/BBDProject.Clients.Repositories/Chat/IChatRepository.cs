using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;

namespace BBDProject.Clients.Repositories.Chat
{
    public interface IChatRepository
    {
        Task<int> SendMessage(string message, int authorId);
        Task<List<DaoMessage>> GetNewMessages(int userId, int lastMessageId);
        Task<List<DaoMessage>> GetPaged(int messagesPerPage, int pageNumber);
    }
}
