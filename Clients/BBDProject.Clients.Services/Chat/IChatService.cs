using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BBDProject.Shared.Models.Chat;

namespace BBDProject.Clients.Services.Chat
{
    public interface IChatService
    {
        Task SendMessage(string message);
        Task<List<MessageModel>> GetAllMessages();
        Task<List<MessageModel>> GetLast();
        Task<List<MessageModel>> GetNotRead();
        Task<List<MessageModel>> GetPaged(int messagesPerPage, int pageNumber);
    }
}
