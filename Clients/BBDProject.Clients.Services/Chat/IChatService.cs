using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BBDProject.Clients.Models.Chat;

namespace BBDProject.Clients.Services.Chat
{
    public interface IChatService
    {
        Task SendMessage(string message);
        Task<List<MessageModel>> GetLast();
        Task<List<MessageModel>> GetPaged(int messagesPerPage, int pageNumber);
        Task<List<MessageModel>> GetPreviousPage(int messagesPerPage);
    }
}
