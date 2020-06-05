using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBDProject.Clients.Models.Chat;
using BBDProject.Clients.Repositories.Chat;
using BBDProject.Shared.Utils.Extensions;
using Ganss.XSS;

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
            message = HtmlSanitizer.Sanitize(message);
            UserContext.LastMessageId = await _chatRepository.SendMessage(message, UserContext.UserId);
            await ChatHub.SendNewMessagesMessage();
        }

        public async Task<List<MessageModel>> GetLast()
        {
            var messages = Mapper.Map<List<MessageModel>>(await _chatRepository.GetNewMessages(UserContext.UserId, UserContext.LastMessageId));
            messages.ForEach(m => m.IsMyMessage = m.AuthorId.Equals(UserContext.UserId));
            if (messages.Any())
                UserContext.LastMessageId = messages.Last().Id;
            return messages;
        }

        public async Task<List<MessageModel>> GetPaged(int messagesPerPage, int pageNumber)
        {
            var messages = Mapper.Map<List<MessageModel>>(await _chatRepository.GetPaged(messagesPerPage, pageNumber));
            UserContext.ChatLastPage = pageNumber;
            messages.ForEach(m => m.IsMyMessage = m.AuthorId.Equals(UserContext.UserId));
            if (messages.Any())
                UserContext.LastMessageId = messages.Last().Id;
            return messages;
        }

        public async Task<List<MessageModel>> GetPreviousPage(int messagesPerPage)
        {
            UserContext.ChatLastPage += 1;
            var messages = Mapper.Map<List<MessageModel>>(await _chatRepository.GetPaged(messagesPerPage, UserContext.ChatLastPage))
                .OrderByDescending(_ => _.DateAdded).ToList();
            messages.ForEach(m => m.IsMyMessage = m.AuthorId.Equals(UserContext.UserId));
            if (messages.Any())
                UserContext.LastMessageId = messages.Last().Id;
            return messages;
        }
    }
}
