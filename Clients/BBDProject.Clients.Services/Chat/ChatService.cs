using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BBDProject.Clients.Repositories.Chat;
using BBDProject.Shared.Models.Chat;

namespace BBDProject.Clients.Services.Chat
{
    public class ChatService : BaseService, IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task SendMessage(string message)
        {
            var messageId = await _chatRepository.SendMessage(message, UserContext.UserId);
        }

        public async Task<List<MessageModel>> GetAllMessages()
        {
            throw new NotImplementedException();
        }

        public async Task<List<MessageModel>> GetLast()
        {
            throw new NotImplementedException();
        }

        public async Task<List<MessageModel>> GetNotRead()
        {
            throw new NotImplementedException();
        }

        public async Task<List<MessageModel>> GetPaged(int messagesPerPage, int pageNumber)
        {
            var messages = await _chatRepository.GetPaged(messagesPerPage, pageNumber);
            return Mapper.Map<List<MessageModel>>(messages);
        }
    }
}
